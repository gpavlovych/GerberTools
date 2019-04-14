using System;

namespace GerberLibrary.Core
{
    public sealed class ExcellonToGCodeConverterConfig
    {
        public ExcellonToGCodeConverterConfig(double drillFeed, double moveFeed, double spindle, double zmin, double zmax)
        {
            if (drillFeed < 10 || drillFeed > 500)
            {
                throw new ArgumentOutOfRangeException(nameof(drillFeed), "drillFeed should be in range 10 to 500");
            }

            if (moveFeed < 10 || moveFeed > 500)
            {
                throw new ArgumentOutOfRangeException(nameof(moveFeed), "moveFeed should be in range 10 to 500");
            }

            if (spindle < 10 || spindle > 3000)
            {
                throw new ArgumentOutOfRangeException(nameof(spindle), "spindle should be in range 10 to 3000");
            }

            if (zmin > zmax)
            {
                throw new ArgumentException("zmin should not exceed zmax");
            }

            this.DrillFeed = drillFeed;
            this.MoveFeed = moveFeed;
            this.Spindle = spindle;
            this.ZMin = zmin;
            this.ZMax = zmax;
        }

        public double ZMax { get; }

        public double ZMin { get; }

        public double Spindle { get; }

        public double MoveFeed { get; }

        public double DrillFeed { get; }
    }
}