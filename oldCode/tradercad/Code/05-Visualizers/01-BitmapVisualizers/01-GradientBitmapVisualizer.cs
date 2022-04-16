using System.Collections.Generic;
using Core;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

namespace Visualizers
{
    public class GradientBitmapVisualizer : IVisualizer
    {
        private const int HeadRowHeight = 25;
        private const int XAxisRow = 25;
        private const int FooterRow = 25;
        private const int YAxisColumnWidth = 40;

        public VisualizerAnswer Read(VisualizerRequest request)
        {
            return new()
            {
                Visualizer = "PngVisualizer",
                Result = VisualizerResult.Ok,
                Bitmap = MakeBitmap(request)
            };
        }

        private static Bitmap MakeBitmap(VisualizerRequest request)
        {
            int pictureWidth = request.PictureSettings.Width;
            int pictureHeight = request.PictureSettings.Height;

            var bitmap = new Bitmap(pictureWidth, pictureHeight);

            using var graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.Clear(Color.FromArgb(0xFF, 0xF9, 0xFF, 0xF9));

            if (request.IndicatorDrawsInSeparateChart is false)
            {
                DrawDataAndIndicator(graphics, request);
            }

            // Draw headline
            DrawHorizontalTextInRectangleAlignLeft(
                graphics,
                request.Headline,
                new Font("Verdana", 100, FontStyle.Bold, GraphicsUnit.Pixel),
                new SolidBrush(Color.FromArgb(0xA0, 0x00, 0x00, 0x00)),
                x1: 0,
                y1: 0,
                x2: pictureWidth,
                y2: HeadRowHeight);

            // Draw watermark
            DrawHorizontalTextInRectangleAlignCenter(
                graphics,
                "TraderCAD.com",
                new Font("Verdana", 300, FontStyle.Bold, GraphicsUnit.Pixel),
                new SolidBrush(Color.FromArgb(0x05, 0x00, 0x00, 0x00)),
                x1: 0,
                y1: 0,
                x2: pictureWidth,
                y2: pictureHeight);
            return bitmap;
        }

        private static void DrawDataAndIndicator(Graphics graphics, VisualizerRequest request)
        {
            var dataLength = request.Data.Length;
            var xStep = (float)request.PictureSettings.Width / dataLength;
            int drawingFieldMaxHeight = request.PictureSettings.Height - XAxisRow - FooterRow;

            var min = decimal.MaxValue;
            var max = decimal.MinValue;

            foreach (FinancePoint p in request.Data)
            {
                if (p.Average > max) { max = p.Average; }
                if (p.Average < min) { min = p.Average; }
            }

            var dataCurve = new Point[dataLength + 2]; // + start point at left-bottom side and stop point at right-bottom side

            dataCurve[0] = new Point(x: 0, y: drawingFieldMaxHeight);

            for (int i = 0; i < dataLength; i++)
            {
                dataCurve[i + 1] = new Point
                    (
                    (int)(i * xStep),
                    (int)Convert(request.Data[i].Average, min, max, drawingFieldMaxHeight, HeadRowHeight));
            }

            dataCurve[dataLength + 1] = new Point((int)((dataLength - 1) * xStep), drawingFieldMaxHeight);

            var gradientBrush = new LinearGradientBrush(
                new Point(0, HeadRowHeight),
                new Point(0, drawingFieldMaxHeight),
                Color.FromArgb(0xA0, 0xE0, 0x2B, 0x1A),
                Color.FromArgb(0xA0, 0x1A, 0x9B, 0xE0));

            graphics.FillPolygon(gradientBrush, dataCurve);

            var indicatorCurve = new List<Point>();

            for (int i = 0; i < dataLength; i++)
            {
                if (request.IndicatorData[i].Value != null)
                {
                    decimal indicatorValue = (decimal)request.IndicatorData[i].Value;

                    indicatorCurve.Add(new Point(
                            (int)(i * xStep),
                            (int)Convert(indicatorValue, min, max, drawingFieldMaxHeight, HeadRowHeight))
                    );
                }
            }

            graphics.DrawLines(new Pen(new SolidBrush(Color.FromArgb(0xFF, 0xFF, 0x00, 0x00)), 3), indicatorCurve.ToArray());
        }

        private static void DrawHorizontalTextInRectangleAlignCenter(Graphics graphics, string text, Font font, Brush brush, int x1, int y1, int x2, int y2)
        {
            var rectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);

            using var stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            font = GetAdjustedFont(graphics, text, font, x1, x2, y1, y2, 200, 6);
            graphics.DrawString(text, font, brush, rectangle, stringFormat);
        }

        private static void DrawHorizontalTextInRectangleAlignLeft(Graphics graphics, string text, Font font, Brush brush, int x1, int y1, int x2, int y2)
        {
            var rectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);

            using var stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Far,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            font = GetAdjustedFont(graphics, text, font, x1, x2, y1, y2, 200, 6);
            graphics.DrawString(text, font, brush, rectangle, stringFormat);
        }

        private static Font GetAdjustedFont(Graphics graphics, string text, Font originalFont, int x1, int x2, int y1, int y2, int maxFontSize, int minFontSize)
        {
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize -= 2)
            {
                var adjustedFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);
                var adjustedSizeNew = graphics.MeasureString(text, adjustedFont);

                if
                (x2 - x1 > System.Convert.ToInt32(adjustedSizeNew.Width) &&
                 y2 - y1 > System.Convert.ToInt32(adjustedSizeNew.Height))
                {
                    return adjustedFont;
                }
            }

            return originalFont;
        }

        // Convert one range of numbers to another
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Convert(decimal value, decimal from1, decimal from2, decimal to1, decimal to2)
            => (value - from1) / (from2 - from1) * (to2 - to1) + to1;
    }
}