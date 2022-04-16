// Read more at TraderCAD.com

using System;

namespace Core
{
    public record DeleteDataConnectorRequest : ICrudConnectorRequest
    {
        public string Equity { get; init; } = null!;
        public TimeSpan Timeframe { get; init; }
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }
        public TimeSpan? TimeoutMs { get; init; }
    };
}