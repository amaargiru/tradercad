// Read more at TraderCAD.com

using System;

namespace Core
{
    public record ReadDataConnectorRequest : ICrudConnectorRequest
    {
        public string Equity { get; init; } = null!;
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }
        public TimeSpan Timeframe { get; init; }
        public TimeSpan? TimeoutMs { get; init; }
        public string? Password { get; init; }
    };
}