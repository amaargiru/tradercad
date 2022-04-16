// Read more at TraderCAD.com

namespace Core
{
    public record AreaOfInterest
    {
        public string Name { get; init; } = null!;
        public PointOfInterest[] PointsOfInterest { get; init; } = null!;
    };
}