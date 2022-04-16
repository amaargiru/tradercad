// Read more at TraderCAD.com

using System;

namespace Core
{
    public record PointOfInterest
    {
        public string Name { get; init; } = null!;
        public DateTime Time { get; init; }
        public decimal Value { get; init; }
    };
}