// Read more at TraderCAD.com

namespace Core
{
    public enum ApiResult
    {
        Ok,
        NoData,
        NotEnoughData,
        NoEquityPresent,
        NoTimeFramePresent,
        NoIndicatorPresent,
        NoVisualizerPresent,
        TimeoutExpired
    }
    public record ApiAnswer
    {
        public string Equity { get; init; } = null!;
        public string Indicator { get; init; } = null!;
        public ApiResult Result { get; init; }
        public byte[]? Picture { get; init; }
        public string? Json { get; init; }
    };
}