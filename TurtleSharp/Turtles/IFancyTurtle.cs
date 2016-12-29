namespace TurtleSharp
{
    public interface IFancyTurtle : ITurtle
    {
        string Stroke { get; set; }
        string StrokeWidth { get; set; }
    }
}
