// Read more at TraderCAD.com

using System;

namespace Core
{
    public record IndicatorRequest
    {
        public string Indicator { get; init; } = null!;
        public FinancePoint[] Data { get; init; } = null!;
        public FinancePoint?[][] ExtraData { get; init; }
        public decimal?[] Coeffs { get; init; }
        public TimeSpan? TimeoutMs { get; init; }
    };
}