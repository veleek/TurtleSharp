namespace TurtleSharp
{
    public class TurtleState
    {
        public PointD Pos { get; set; }
        public double Rot { get; set; }

        public TurtleState() : this(new PointD(), 0) { }

        public TurtleState(PointD p, double r)
        {
            this.Pos = new PointD(p);
            this.Rot = r;
        }

        public TurtleState(ITurtle t) : this(t.Pos, t.Rot)
        {
        }

        public void ApplyTo(ITurtle t)
        {
            t.Pos = this.Pos;
            t.Rot = this.Rot;
        }
    }
}
