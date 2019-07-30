using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using GerberLibrary.Core.Primitives;

namespace GerberLibrary.Core
{
    public sealed class ExcellonPoint
    {
        internal ExcellonPoint(double? x, double? y, string tool)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            Point = new PointD(x.Value, y.Value);
            Tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public PointD Point { get; }
        public string Tool { get; }
    }

    internal sealed class ExcellonReaderEnumerator : IEnumerator<ExcellonPoint>
    {
        private readonly TextReader reader;
        private double? currentX = null;
        private double? currentY = null;
        private string currentTool = null;
        private readonly Dictionary<int, string> toolInfo = new Dictionary<int, string>();
        private int? leadingZeroes = null;
        private int? trailingZeroes = null;

        public ExcellonReaderEnumerator(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            this.reader = reader;
        }

        public ExcellonPoint Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            this.reader.Dispose();
        }

        public void Init()
        {
            string currentLine;
            while ((currentLine = this.reader.ReadLine()) != null && (currentLine != "%"))
            {
                if (Regex.IsMatch(currentLine, @"METRIC,\d+\.\d+", RegexOptions.IgnoreCase))
                {
                    currentLine = currentLine.Remove(0, "METRIC,".Length);
                    leadingZeroes = currentLine.IndexOf(".", StringComparison.OrdinalIgnoreCase);
                    trailingZeroes = currentLine.Length - leadingZeroes - 1;
                    continue;
                }

                if (string.Equals(currentLine, @"METRIC,LZ", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var fileFormatRegex = Regex.Match(currentLine,
                    @";FILE_FORMAT=(?<leadingZeroes>\d+):(?<trailingZeroes>\d+)", RegexOptions.IgnoreCase);
                if (fileFormatRegex.Success)
                {
                    leadingZeroes = int.Parse(fileFormatRegex.Groups["leadingZeroes"].Value);
                    trailingZeroes = int.Parse(fileFormatRegex.Groups["trailingZeroes"].Value);
                    continue;
                }

                var toolInfoRegex = Regex.Match(currentLine,
                    @"T(?<toolNumber>\d+)F00S00C(?<toolDiameter>\d+\.\d+)", RegexOptions.IgnoreCase);
                if (toolInfoRegex.Success)
                {
                    var toolNumber = int.Parse(toolInfoRegex.Groups["toolNumber"].Value);
                    var toolDiameter = decimal.Parse(toolInfoRegex.Groups["toolDiameter"].Value);
                    toolInfo[toolNumber] = $"drill dia. {toolDiameter} mm";
                    continue;
                }
            } //TODO: more sophisticated approach needed: separate NC files! for different tools. See https://gist.github.com/katyo/5692b935abc085b1037e
        }
        private double? ParseCoordinate(string line, string coord)
        {
            if (trailingZeroes == null || leadingZeroes == null)
            {
                throw new ArgumentException("leading and trailing zeroes not configured");
            }

            var coordValueRegex =
                Regex.Match(line, $@"{coord}(?<coordValue>[+-]?\d+)", RegexOptions.IgnoreCase);
            if (!coordValueRegex.Success)
            {
                return null;
            }

            double result = int.Parse(coordValueRegex.Groups["coordValue"].Value);
            for (var i = 0; i < trailingZeroes; i++)
            {
                result /= 10.0;
            }
            return result;
        }
        public bool MoveNext()
        {
            var newTool = this.currentTool;
            var newX = this.currentX;
            var newY = this.currentY;
            while (newTool == null || newX == null || newY == null || (newTool == currentTool &&
                   (newX == this.currentX) &&
                   (newY == this.currentY)))
            {
                var currentLine = this.reader.ReadLine();
                if (currentLine == null || (currentLine == "M30"))
                {
                    return false;
                }

                var useToolRegex = Regex.Match(currentLine, @"T(?<drillNumber>\d+)");
                if (useToolRegex.Success)
                {
                    var drillNumber = int.Parse(useToolRegex.Groups["drillNumber"].Value);
                    newTool = toolInfo.ContainsKey(drillNumber)
                        ? toolInfo[drillNumber]
                        : drillNumber.ToString();
                }
                else
                {
                    newX = ParseCoordinate(currentLine, "X") ?? this.currentX;
                    newY = ParseCoordinate(currentLine, "Y") ?? this.currentY;
                }
            }

            this.currentTool = newTool;
            this.currentX = newX;
            this.currentY = newY;

            this.Current = new ExcellonPoint(this.currentX, this.currentY, this.currentTool);
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
    public sealed class ExcellonReader: IEnumerable<ExcellonPoint>
    {
        private readonly TextReader reader;

        public ExcellonReader(TextReader reader)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IEnumerator<ExcellonPoint> GetEnumerator()
        {
            var result = new ExcellonReaderEnumerator(reader);
            result.Init();
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
