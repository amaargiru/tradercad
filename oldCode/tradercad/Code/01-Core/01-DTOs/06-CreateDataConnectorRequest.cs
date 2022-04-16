// Read more at TraderCAD.com

using System;

namespace Core
{
    public record CreateDataConnectorRequest : ICrudConnectorRequest
    {
        public string Equity { get; init; } = null!;
        public TimeSpan Timeframe { get; init; }
        public FinancePoint[] Data { get; init; } = null!;
        public TimeSpan? TimeoutMs { get; init; }
    };
}