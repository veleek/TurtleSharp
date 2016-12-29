using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace TurtleSharp
{
    public interface ITurtle
    {
        PointD Pos { get; set; }

        double Rot { get; set; }

        double Scale { get; set; }

        bool IsPenDown { get; set; }

        void Reset();

        void Push(TurtleState state);

        TurtleState Pop();

        TurtleState Peek();
    }

    public static class TurtleMovementExtensions
    {
        public static void GoTo(this ITurtle turtle, PointD p)
        {
            turtle.GoTo(p.X, p.Y);
        }

        public static void GoTo(this ITurtle turtle, double x, double y)
        {
            turtle.Pos = new PointD(x, y);
        }

        public static void PenDown(this ITurtle turtle)
        {
            if (!turtle.IsPenDown)
            {
                turtle.IsPenDown = true;
            }
        }

        public static void PenUp(this ITurtle turtle)
        {
            turtle.IsPenDown = false;
        }

        public static void Forward(this ITurtle turtle, double distance)
        {
            distance *= turtle.Scale;
            turtle.GoTo(
                turtle.Pos.X + distance * Sin(turtle.Rot),
                turtle.Pos.Y + distance * Cos(turtle.Rot));
        }

        public static void Back(this ITurtle turtle, double distance)
        {
            turtle.Forward(-distance);
        }

        public static void Rotate(this ITurtle turtle, double angle)
        {
            turtle.Rot = (turtle.Rot + (2 * PI) + (-angle % (2 * PI))) % (2 * PI);
        }

        public static void Left(this ITurtle turtle, double angle)
        {
            turtle.Rotate(-angle);
        }

        public static void Right(this ITurtle turtle, double angle)
        {
            turtle.Rotate(angle);
        }

        public static void GoHome(this ITurtle turtle)
        {
            TurtleState state = turtle.Peek();
            if(state != null)
            {
                state.ApplyTo(turtle);
            }
        }

        public static void Reset(this ITurtle turtle)
        {
            turtle.IsPenDown = false;
            turtle.Push(new TurtleState());
            turtle.GoHome();
        }

        public static void Push(this ITurtle turtle)
        {
            turtle.Push(new TurtleState(turtle));
        }
    }
}
