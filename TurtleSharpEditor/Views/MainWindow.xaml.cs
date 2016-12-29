using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using AurelienRibon.Ui.SyntaxHighlightBox;

namespace TurtleSharpEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            this.CodeTextBox.CurrentHighlighter = HighlighterManager.Instance.Highlighters["CSharp"];
            this.CodeTextBox.Text = @"using AurelienRibon.Ui.SyntaxHighlightBox;

namespace TurtleSharpEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            this.CodeTextBox.CurrentHighlighter = HighlighterManager.Instance.Highlighters[""CSharp""];
            this.CodeTextBox.Text = @""...""

        }
    }
}";

            Program p = new Program();
            Bitmap bmp = p.Main();
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
            this.OutputImage.Source = bs;
        }
    }
}
