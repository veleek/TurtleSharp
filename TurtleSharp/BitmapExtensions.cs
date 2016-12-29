using System.Drawing;

namespace TurtleSharp
{
    public static class BitmapExtensions
    {
        public static Bitmap Resize(this Bitmap imgToResize, double scale)
        {
            Bitmap b = new Bitmap((int)(imgToResize.Width * scale), (int)(imgToResize.Height * scale));
            using (Graphics g = Graphics.FromImage(b))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, b.Width, b.Height);
            }
            return b;
        }
    }
}
