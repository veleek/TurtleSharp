namespace TurtleSharp
{
    public abstract class TurtleShell<TTurtle> : ITurtle
        where TTurtle : ITurtle
    {
        public TTurtle Turtle;

        public TurtleShell(TTurtle turtle)
        {
            this.Turtle = turtle;
        }

        PointD ITurtle.Pos
        {
            get { return Turtle.Pos; }
            set { Turtle.Pos = value; }
        }

        double ITurtle.Rot
        {
            get { return Turtle.Rot; }
            set { Turtle.Rot = value; }
        }

        public double Scale
        {
            get { return Turtle.Scale; }
            set { Turtle.Scale = value; }
        }

        public bool IsPenDown
        {
            get { return this.Turtle.IsPenDown; }
            set { this.Turtle.IsPenDown = value; }
        }

        public void Reset()
        {
            Turtle.Reset();
        }

        public void Push(TurtleState state)
        {
            Turtle.Push(state);
        }

        public TurtleState Pop()
        {
            return Turtle.Pop();
        }

        public TurtleState Peek()
        {
            return Turtle.Peek();
        }
    }

    public class TurtleShell : TurtleShell<ITurtle>
    {
        public TurtleShell(ITurtle turtle) : base(turtle) { }
    }
}
