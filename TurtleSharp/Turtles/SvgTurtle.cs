using System;
using System.Drawing;
using System.IO;
using System.Text;
using Svg;

namespace TurtleSharp
{
    public class SvgTurtle : Turtle, IFancyTurtle
    {
        public StringBuilder svgPathData = new StringBuilder();
        public StringBuilder svgContent = new StringBuilder();

        public string Stroke { get; set; } = "#000000";

        public string StrokeWidth { get; set; } = "1pt";

        public string DocumentWidth { get; set; }
        public string DocumentHeight { get; set; }

        public override PointD Pos
        {
            get
            {
                return base.Pos;
            }

            set
            {
                base.Pos = value;

                if(this.IsPenDown)
                {
                    svgPathData.AppendFormat("{0} ", this.Pos);
                }
            }
        }

        public override bool IsPenDown
        {
            get { return base.IsPenDown; }
            set
            {
                bool wasDown = base.IsPenDown;
                base.IsPenDown = value;
                if (!wasDown && base.IsPenDown)
                {
                    svgPathData.AppendFormat("M {0} ", this.Pos);
                }
                else if (wasDown && !base.IsPenDown)
                {
                    if (svgPathData.Length > 0)
                    {
                        svgPathData.AppendLine("z");
                    }
                }
            }
        }

        public void StartGroup()
        {
            svgContent.AppendLine("<g>");
        }

        public void EndGroup()
        {
            svgContent.AppendLine("</g>");
        }

        public void EndPath()
        {
            if (svgPathData.Length > 0)
            {
                string pathData = svgPathData.ToString();
                svgContent.AppendFormat(@"<path style=""fill:none;stroke:{0};stroke-width:{1};stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1"" d=""{2}""/>", Stroke, StrokeWidth, pathData);
                svgContent.AppendLine();
                svgPathData = new StringBuilder();
            }
        }

        public void InsertImage(string path, double w, double h)
        {
            InsertImage(path, w, h, this.Pos.X, this.Pos.Y);
        }

        public void InsertImage(string path, double w, double h, double x, double y)
        {
            const string imageElementTemplate =
                @"<image x=""{0}"" y=""{1}""
                      width=""{2}"" height=""{3}"" 
                      xlink:href=""data:image/png;base64,{4}""
                      style=""image-rendering:optimizeSpeed""
                      preserveAspectRatio=""none"" />";

            byte[] imageData = File.ReadAllBytes(path);
            string imageBase64 = Convert.ToBase64String(imageData);

            svgContent.AppendFormat(imageElementTemplate, x, y, w, h, imageBase64);
            svgContent.AppendLine();
        }

        public void InsertText(char text, string fill = null, string stroke = null, string fontFamily = null, double? fontSize = null, double? x = null, double? y = null, double? xOffset = null, double? yOffset = null, double? rotate = null, string textAnchor = null)
        {
            InsertText(text.ToString(), fill, stroke, fontFamily, fontSize, x, y, xOffset, yOffset, rotate, textAnchor);
        }

        public void InsertText(string text, string fill = null, string stroke = null, string fontFamily = null, double? fontSize = null, double? x = null, double? y = null, double? xOffset = null, double? yOffset = null, double? rotate = null, string textAnchor = null)
        {
            StringBuilder textElement = new StringBuilder("<text");

            double xPos = (x ?? this.Pos.X) + (xOffset ?? 0);
            double yPos = (y ?? this.Pos.Y) + (yOffset ?? 0);

            textElement.Append($@" font-family=""{fontFamily ?? "Arial"}""");
            textElement.Append($@" font-size=""{fontSize ?? 20}""");
            textElement.Append($@" x=""{xPos}"" y=""{yPos}""");

            if (fill != null) textElement.Append($@" fill=""{fill}""");
            if (stroke != null) textElement.Append($@" stroke=""{stroke}""");
            if (rotate != null) textElement.Append($@" transform=""rotate(90 {xPos},{yPos})""");
            if (textAnchor != null) textElement.Append($@" text-anchor=""{textAnchor}""");
            //textElement.Append($@" alignment-baseline=""middle""");

            textElement.Append($@">{text}</text>");
            svgContent.AppendLine(textElement.ToString());
        }

        public override void Reset()
        {
            this.EndPath();
            base.Reset();
        }

        public void Clear()
        {
            this.Reset();
            this.svgContent = new StringBuilder();
        }

        public override string ToString()
        {
            return svgContent.ToString();
        }

        public string ToSvg()
        {
            if (this.svgPathData.Length > 0)
            {
                this.EndPath();
            }

            string svgData =
            $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
               <svg xmlns=""http://www.w3.org/2000/svg"" 
                    xmlns:xlink=""http://www.w3.org/1999/xlink""
                    version=""1.1""
                    width=""{this.DocumentWidth ?? "24in"}""
                    height=""{this.DocumentHeight ?? "18in"}"">
                   {this.ToString()}
               </svg>";

            return svgData;
        }

        public Bitmap ToBitmap()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToSvg())))
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                return svgDocument.Draw();
            }
        }

        public void SaveSvg(string path)
        {
            File.WriteAllText(path, this.ToSvg());
        }
    }
}
