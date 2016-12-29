using System;

namespace TurtleSharp
{
    public static class Pentagon
    {
        public static readonly double Apothem = Polygon.Apothem(1, 5);
        public static readonly double Side = Polygon.SideLength(1, 5);
    }

    public class Polygon
    {
        public static double SideLength(double radius, int n)
        {
            return 2 * radius * Math.Sin(Math.PI / n);
        }

        public static double Apothem(double radius, int n)
        {
            return radius * Math.Cos(Math.PI / n);
        }

        public static double Area(double radius, int n)
        {
            return n * radius * radius * Math.Sin(2 * Math.PI / n) / 2;
        }
    }
}
