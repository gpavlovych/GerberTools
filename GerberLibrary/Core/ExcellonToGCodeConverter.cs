using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
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
            using (Start(gcode))
            {
                double? lastX = null;
                double? lastY = null;
                string lastTool = null;
                foreach (var point in new ExcellonReader(excellon))
                {
                    var toolChanged = !string.Equals(lastTool, point.Tool, StringComparison.OrdinalIgnoreCase);
                    var xChanged = lastX != point.Point.X;
                    var yChanged = lastY != point.Point.Y;
                    if (toolChanged)
                    {
                        gcode.WriteLine($"; use tool: {point.Tool}");
                        lastTool = point.Tool;
                    }

                    if (xChanged || yChanged)
                    {
                        gcode.Write("G1 ");
                        if (xChanged)
                        {
                            gcode.Write($"X{point.Point.X}");
                            lastX = point.Point.X;
                        }

                        if (yChanged)
                        {
                            gcode.Write($"Y{point.Point.Y}");
                            lastY = point.Point.Y;
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