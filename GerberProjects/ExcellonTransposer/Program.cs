using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLineParser.Arguments;
using CommandLineParser.Exceptions;

namespace ExcellonTransposer
{
    class Program
    {
        static int Main(string[] args)
        {
            var parser = new CommandLineParser.CommandLineParser(); //switch argument is meant for true/false logic
            var inputFile = new FileArgument('i', "input", "Input Excellon file")
            {
                FileMustExist = true, Optional = false
            };
            var inputRealFile = new FileArgument('i', "input", "Input file with 2 imaginary and real points")
            {
                FileMustExist = true, Optional = false
            };
            var outputFile = new FileArgument('o', "output", "Output Excellon file with all real coordinates")
            {
                FileMustExist = false, Optional = false
            };
            parser.Arguments.Add(inputFile);
            parser.Arguments.Add(outputFile);
            try
            {
                parser.ParseCommandLine(args);
                using (var reader = inputFile.Value.OpenText())
                using (var writer = new StreamWriter(outputFile.Value.OpenWrite()))
                {
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
