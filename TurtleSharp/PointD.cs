using System;

namespace TurtleSharp
{
    public class PointD
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointD()
        {
        }

        public PointD(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public PointD(PointD other) : this(other.X, other.Y)
        {
        }

        public override string ToString()
        {
            return $"{Math.Round(this.X, 2)},{Math.Round(this.Y, 2)}";
        }
    }
}
