using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace GerberLibrary.Core
{
    public sealed class ExcellonToGCodeConverter
    {
        private readonly ExcellonToGCodeConverterConfig config;

        public ExcellonToGCodeConverter(ExcellonToGCodeConverterConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private IDisposable Start(TextWriter writer)
        {
            writer.WriteLine("G90");
            writer.WriteLine($"G1 Z{this.config.ZMax} F{this.config.MoveFeed} ");
            writer.WriteLine($"M3 S{this.config.Spindle}");

            return new FinalAction(() => writer.WriteLine("M5"));
        }

        public void Convert(TextReader excellon, TextWriter gcode)
        {
                string currentLine;
                int? leadingZeroes = null;
                int? trailingZeroes = null;
                while ((currentLine = excellon.ReadLine()) != null && (currentLine != "T01"))
                {
                    if (currentLine.StartsWith("METRIC,", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = currentLine.Remove(0, "METRIC,".Length);
                        leadingZeroes = currentLine.IndexOf(".", StringComparison.OrdinalIgnoreCase);
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

                using (Start(gcode))
                {
                    while ((currentLine = excellon.ReadLine()) != null && (currentLine != "M30"))
                    {
                        gcode.Write("G1 ");
                        double? x = ParseCoordinate("X");
                        if (x != null)
                        {
                            gcode.Write($"X{x}");
                        }

                        double? y = ParseCoordinate("Y");
                        if (y != null)
                        {
                            gcode.Write($"Y{y}");
                        }

                        gcode.WriteLine();

                        gcode.WriteLine($"G1 Z{this.config.ZMin} F{this.config.DrillFeed}");
                        gcode.WriteLine($"G1 Z{this.config.ZMax} F{this.config.MoveFeed}");
                    }
                }
            }
        }
    }
}