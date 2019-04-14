using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLineParser.Arguments;
using CommandLineParser.Exceptions;

namespace ExcellonToGrbl
{
    internal sealed class FinalAction : IDisposable
    {
        private readonly Action finalAction;

        public FinalAction(Action finalAction)
        {
            this.finalAction = finalAction ?? throw new ArgumentNullException(nameof(finalAction));
        }

        public void Dispose()
        {
            this.finalAction();
        }
    }
    class Program
    {
        static IDisposable Start(TextWriter writer, double moveFeed, double zMax, double spindleSpeed)
        {
            writer.WriteLine("G90");
            writer.WriteLine($"G1 Z{zMax} F{moveFeed} ");
            writer.WriteLine($"M3 S{spindleSpeed}");

            return new FinalAction(() => writer.WriteLine("M5"));
        }
        static int Main(string[] args)
        {
            var settings = Properties.Settings.Default;
            CommandLineParser.CommandLineParser parser = new CommandLineParser.CommandLineParser(); //switch argument is meant for true/false logic
            FileArgument inputFile = new FileArgument('i', "input", "Input Excellon file");
            inputFile.FileMustExist = true;
            inputFile.Optional = false;
            FileArgument outputFile = new FileArgument('o', "output", "Output GRBL file");
            outputFile.FileMustExist = false;
            outputFile.Optional = false;
            ValueArgument<double> maximumZ = new ValueArgument<double>("zmax");
            maximumZ.DefaultValue = settings.zmax;
            ValueArgument<double> minimumZ = new ValueArgument<double>("zmin");
            minimumZ.DefaultValue = settings.zmin;
            parser.Arguments.Add(inputFile);
            parser.Arguments.Add(outputFile);
            parser.Arguments.Add(minimumZ);
            parser.Arguments.Add(maximumZ);
            try
            {
                parser.ParseCommandLine(args);
                using (var reader = inputFile.Value.OpenText())
                {
                    string currentLine;
                    int? leadingZeroes = null;
                    int? trailingZeroes = null;
                    while (!reader.EndOfStream && (currentLine = reader.ReadLine()) != "T01")
                    {
                        if (currentLine.StartsWith("METRIC,"))
                        {
                            currentLine = currentLine.Remove(0, "METRIC,".Length);
                            leadingZeroes = currentLine.IndexOf(".");
                            trailingZeroes = currentLine.Length - leadingZeroes - 1;
                        }
                    } //TODO: more sophisticated approach needed: separate NC files! for different tools. See https://gist.github.com/katyo/5692b935abc085b1037e

                    if (leadingZeroes == null)
                    {
                        throw new ArgumentException("leading and trailing zeroes not configured");
                    }

                    double? ParseCoordinate(string coord)
                    {
                        if (!currentLine.StartsWith(coord, true, CultureInfo.InvariantCulture))
                        {
                            return null;
                        }

                        double result =
                            int.Parse(currentLine.Substring(1, leadingZeroes.Value + trailingZeroes.Value));
                        for (var i = 0; i < trailingZeroes; i++)
                        {
                            result /= 10.0;
                        }

                        currentLine = currentLine.Remove(0, 1 + leadingZeroes.Value + trailingZeroes.Value);
                        return result;
                    }

                    using (var writer = new StreamWriter(outputFile.Value.OpenWrite()))
                    using (Start(writer, settings.moveFeed, maximumZ.Value, settings.spindle))
                    {
                        while (!reader.EndOfStream && (currentLine = reader.ReadLine()) != "M30")
                        {
                            writer.Write("G1 ");
                            double? x = ParseCoordinate("X");
                            if (x != null)
                            {
                                writer.Write($"X{x}");
                            }

                            double? y = ParseCoordinate("Y");
                            if (y != null)
                            {
                                writer.Write($"Y{y}");
                            }

                            writer.WriteLine();

                            writer.WriteLine($"G1 Z{minimumZ.Value} F{settings.drillFeed}");
                            writer.WriteLine($"G1 Z{maximumZ.Value} F{settings.moveFeed}");
                        }
                    }
                }

                return 0;
            }
            catch (CommandLineException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
    }
}
