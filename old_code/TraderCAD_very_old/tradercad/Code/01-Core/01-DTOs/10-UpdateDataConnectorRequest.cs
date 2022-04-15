// Read more at TraderCAD.com

using System;

namespace Core
{
    public record UpdateDataConnectorRequest : ICrudConnectorRequest
    {
        public string Equity { get; init; } = null!;
        public FinancePoint[] Data { get; init; } = null!;
        public TimeSpan Timeframe { get; init; }
        public TimeSpan? TimeoutMs { get; init; }
    };
}