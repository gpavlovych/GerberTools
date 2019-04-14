using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLineParser.Arguments;
using CommandLineParser.Exceptions;
using GerberLibrary.Core;

namespace ExcellonToGrbl
{
    class Program
    {
       
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
                var xlnConfig = new ExcellonToGCodeConverterConfig(
                    settings.drillFeed, 
                    settings.moveFeed,
                    settings.spindle,
                    minimumZ.Value, 
                    maximumZ.Value);
                var xlnConverter = new ExcellonToGCodeConverter(xlnConfig);
                using (var reader = inputFile.Value.OpenText())
                using (var writer = new StreamWriter(outputFile.Value.OpenWrite()))
                {
                    xlnConverter.Convert(reader, writer);
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
