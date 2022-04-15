// Read more at TraderCAD.com

namespace Core
{
    public enum IndicatorResult
    {
        Ok,
        WrongData,
        NotEnoughData,
        TimeoutExpired
    }
    public record IndicatorAnswer
    {
        public string Indicator { get; init; } = null!;
        public IndicatorResult Result { get; init; }
        public IndicatorPoint?[] Data { get; init; }
        public AreaOfInterest? AreaOfInterest { get; init; }
    };
}