using TraderCadCore;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Visualizer;

public class GradientBitmapVisualizer
{
    private const int HeadRowHeight = 25;
    private const int XAxisRow = 25;
    private const int FooterRow = 25;

    public Bitmap Read(EquityPoint[] data, IndicatorPoint[] indicator, string headline, int pictureWidth, int pictureHeight)
    {
        var bitmap = new Bitmap(pictureWidth, pictureHeight);

        using var graphics = Graphics.FromImage(bitmap);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        graphics.Clear(Color.FromArgb(0xFF, 0xF9, 0xFF, 0xF9));

        DrawDataAndIndicator(graphics, data, indicator, pictureWidth, pictureHeight);

        // Draw headline
        DrawHorizontalTextInRectangleAlignLeft(
            graphics,
            headline,
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

    private static void DrawDataAndIndicator(Graphics graphics, EquityPoint[] data, IndicatorPoint[] indicator, int pictureWidth, int pictureHeightt)
    {
        var dataLength = data.Length;
        var xStep = (float)pictureWidth / dataLength;
        var drawingFieldMaxHeight = pictureHeightt - XAxisRow - FooterRow;

        var averages = Utility.CalculateAverage(data);
        var max = averages.Max();
        var min = averages.Min();

        var dataCurve = new Point[dataLength + 2]; // + start point at left-bottom side and stop point at right-bottom side
        dataCurve[0] = new Point(x: 0, y: drawingFieldMaxHeight);

        for (var i = 0; i < dataLength; i++)
        {
            dataCurve[i + 1] = new Point
                (
                (int)(i * xStep),
                (int)Utility.ConvertOneRangeToAnother(averages[i], min, max, drawingFieldMaxHeight, HeadRowHeight));
        }

        dataCurve[dataLength + 1] = new Point((int)((dataLength - 1) * xStep), drawingFieldMaxHeight);

        var gradientBrush = new LinearGradientBrush(
            new Point(0, HeadRowHeight),
            new Point(0, drawingFieldMaxHeight),
            Color.FromArgb(0xA0, 0xE0, 0x2B, 0x1A),
            Color.FromArgb(0xA0, 0x1A, 0x9B, 0xE0));

        graphics.FillPolygon(gradientBrush, dataCurve);

        var indicatorCurve = new List<Point>();

        for (var i = 0; i < dataLength; i++)
        {
            if (indicator[i].Value != null)
            {
                var indicatorValue = (decimal)indicator[i].Value;

                indicatorCurve.Add(new Point(
                        (int)(i * xStep),
                        (int)Utility.ConvertOneRangeToAnother(indicatorValue, min, max, drawingFieldMaxHeight, HeadRowHeight))
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
        for (var adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize -= 2)
        {
            var adjustedFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);
            var adjustedSizeNew = graphics.MeasureString(text, adjustedFont);

            if
            (x2 - x1 > Convert.ToInt32(adjustedSizeNew.Width) &&
             y2 - y1 > Convert.ToInt32(adjustedSizeNew.Height))
            {
                return adjustedFont;
            }
        }

        return originalFont;
    }
}
