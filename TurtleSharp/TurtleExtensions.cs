using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace TurtleSharp
{
    public static class TurtleShapeExtensions
    {
        public static void RegularPolygon<TTurtle>(this TTurtle t, double radius, int n, Action<TTurtle, int, double> sideAction = null, Action<TTurtle, int> atPoint = null)
            where TTurtle : ITurtle
        {
            t.Forward(radius);
            t.Right(PI / 2);
            t.Right(PI / n);

            if (sideAction == null)
            {
                t.PenDown();
            }

            for (int i = 0; i < n; i++)
            {
                var prevRot = t.Rot;

                if (sideAction == null)
                {
                    t.Forward(Polygon.SideLength(radius, n));
                }
                else
                {
                    sideAction(t, i, Polygon.SideLength(radius, n));
                }

                if (atPoint != null)
                {
                    t.Push();
                    atPoint(t, i);
                    t.Pop();
                }

                t.Rot = prevRot;
                t.Right(2 * PI / n);
            }

            if (sideAction == null)
            {
                t.PenUp();
            }
        }

        public static void Tooth(this Turtle t, double width, double height, double kerf = 0, bool swap = false)
        {
            // Kerf should stay the same regardless of scale, so multiply the kerf by 1/scale
            kerf /= t.Scale;

            t.Forward(width - 2 * kerf);
            t.Left(PI / 2);
            t.Forward(height);
            t.Right(PI / 2);
            t.Forward(width + 2 * kerf);
            t.Right(PI / 2);
            t.Forward(height);
            t.Left(PI / 2);
        }

        public static void Teeth(this Turtle t, int count, double width, double height, double kerf = 0)
        {
            for (int i = 0; i < count; i++)
            {
                t.Tooth(width, height, kerf);
            }
        }

        public static void Arrow(this Turtle t, double length, double headLength)
        {
            t.Back(length / 2);
            t.PenDown();
            t.Forward(length);
            t.PenUp();

            if (headLength > 0)
            {
                t.Right(3 * PI / 4);
                t.Forward(headLength);
                t.PenDown();
                t.Back(headLength);
                t.Right(PI / 2);
                t.Forward(headLength);
                t.Back(headLength);
            }
            t.PenUp();
        }

        public static void Rectangle<TTurtle>(this TTurtle t, double width, double height, Action<TTurtle> atPoint = null)
            where TTurtle : Turtle
        {
            for (int i = 0; i < 2; i++)
            {
                t.Forward(width);
                t.Right(PI / 2);
                atPoint?.Invoke(t);
                t.Forward(height);
                t.Right(PI / 2);
                atPoint?.Invoke(t);
            }
        }
    }
}
