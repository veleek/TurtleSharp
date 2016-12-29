using System.Collections.Generic;

namespace TurtleSharp
{
    public class Turtle : ITurtle
    {
        private Stack<TurtleState> stateStack = new Stack<TurtleState>();
        private TurtleState home = new TurtleState();

        public virtual PointD Pos { get; set; } = new PointD();

        public virtual double Rot { get; set; } = 0.0;

        public virtual double Scale { get; set; } = 1.0;

        public virtual bool IsPenDown { get; set; } = false;

        public virtual void Reset()
        {
            this.IsPenDown = false;
            stateStack = new Stack<TurtleState>();
            this.Push(new TurtleState());
        }

        public void Push(TurtleState state)
        {
            state.ApplyTo(this);
            this.stateStack.Push(state);
        }

        public TurtleState Pop()
        {
            if (this.stateStack.Count == 0)
            {
                return null;
            }

            var state = this.stateStack.Pop();
            state.ApplyTo(this);
            return state;
        }

        public TurtleState Peek()
        {
            return this.stateStack.Peek();
        }
    }

}
