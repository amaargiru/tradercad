// Read more at TraderCAD.com

using System;

namespace Core
{
    public record IndicatorPoint
    {
        public DateTime PointDateTime { get; set; }
        public decimal? Value { get; set; }
    };
}