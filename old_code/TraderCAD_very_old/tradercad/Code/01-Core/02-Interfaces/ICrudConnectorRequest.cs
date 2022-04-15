using System;

namespace Core
{
    public interface ICrudConnectorRequest
    {
        public string Equity { get; init; }
        public TimeSpan Timeframe { get; init; }
    }
}