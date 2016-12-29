using System;
using System.Collections.Generic;

namespace TurtleSharp
{
    public class LaserTurtle : SvgTurtle
    {
        private LaserStrokeType strokeType;

        public LaserTurtle(LaserProfile profile)
        {
            this.Profile = profile;
        }

        public LaserProfile Profile { get; set; }

        public LaserStrokeType StrokeType
        {
            get { return this.strokeType; }
            set
            {
                this.strokeType = value;
                this.Stroke = this.Profile.StrokeTypes[this.strokeType].Item1 ?? this.Stroke;
                this.StrokeWidth = this.Profile.StrokeTypes[this.strokeType].Item2 ?? this.StrokeWidth;
            }
        }
    }

    public class LaserProfile
    {
        public string Name { get; set; }

        public Dictionary<LaserStrokeType, Tuple<string, string>> StrokeTypes;

        public double Kerf { get; set; }
    }

    public class LaserProfiles
    {
        public static LaserProfile Epilog = new LaserProfile
        {
            Name = "Epilog",
            StrokeTypes = new Dictionary<LaserStrokeType, System.Tuple<string, string>>
        {
            { LaserStrokeType.Raster, Tuple.Create("#000000", (string)null) },
            { LaserStrokeType.Cut, Tuple.Create("#000000", "0.01pt") },
        }
        };

        public static LaserProfile Universal = new LaserProfile
        {
            Name = "Universal",
            StrokeTypes = new Dictionary<LaserStrokeType, System.Tuple<string, string>>
        {
            { LaserStrokeType.Raster, Tuple.Create("#000000", (string)null) },
            { LaserStrokeType.Mark, Tuple.Create("#0000FF", "0.01pt") },
            { LaserStrokeType.Cut, Tuple.Create("#FF0000", "0.01pt") },
        }
        };

        // More visible lines for testing
        public static LaserProfile Test = new LaserProfile
        {
            Name = "Test",
            StrokeTypes = new Dictionary<LaserStrokeType, System.Tuple<string, string>>
        {
            { LaserStrokeType.Raster, Tuple.Create("#000000", (string)null) },
            { LaserStrokeType.Mark, Tuple.Create("#0000FF", "1pt") },
            { LaserStrokeType.Cut, Tuple.Create("#FF0000", "1pt") },
        }
        };
    }

    public enum LaserStrokeType
    {
        None,
        Raster,
        Mark,
        Cut,
    }
}
