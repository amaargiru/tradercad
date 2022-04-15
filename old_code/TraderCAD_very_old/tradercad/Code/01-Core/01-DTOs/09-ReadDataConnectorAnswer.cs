// Read more at TraderCAD.com

namespace Core
{
    public enum ReadDataConnectorResult
    {
        Ok,
        NoData,
        NotEnoughData,
        EquityNotExist,
        TimeframeNotExist,
        TimeoutExpired
    }
    public record ReadDataConnectorAnswer
    {
        public string Equity { get; init; } = null!;
        public ReadDataConnectorResult Result { get; init; }
        public FinancePoint[]? Data { get; init; }
    };
}