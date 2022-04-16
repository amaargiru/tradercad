// Read more at TraderCAD.com

using System.Drawing;

namespace Core
{
    public record PictureSettings
    {
        public int Width { get; init; }
        public int Height { get; init; }

        public Color? BackgroundColor { get; init; }
        public Color? PositiveColor { get; init; }
        public Color? NegativeColor { get; init; }
        public Color? IndicatorColor { get; init; }

        public bool? BackgroundLinearGradient { get; init; }
        public Color? BackgroundLinearGradientColorUp { get; init; }
        public Color? BackgroundLinearGradientColorDown { get; init; }

        public bool? AreaOfInterestRadialGradient { get; init; }
        public Color? AreaOfInterestRadialGradientCenterColor { get; init; }
        public Color? AreaOfInterestRadialGradientPeripheralColor { get; init; }
    };
}