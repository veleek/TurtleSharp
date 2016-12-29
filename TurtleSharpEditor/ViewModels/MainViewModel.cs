using System.Drawing;
using Caliburn.Micro;

namespace TurtleSharpEditor.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            this.DisplayName = "Turtle# Editor";
        }

        public Bitmap OutputImage { get; set; }

        public void Execute()
        {
            // See Roslyn API stuff for details on how to compile and run the code
            // https://github.com/dotnet/roslyn/wiki/Scripting-API-Samples#parameter
        }
    }
}
