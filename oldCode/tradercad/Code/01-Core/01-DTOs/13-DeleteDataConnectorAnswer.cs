// Read more at TraderCAD.com

namespace Core
{
    public enum DeleteDataConnectorResult
    {
        Ok,
        EquityNotExist,
        TimeframeNotExist,
        TimeoutExpired
    }
    public record DeleteDataConnectorAnswer
    {
        public string Equity { get; init; } = null!;
        public DeleteDataConnectorResult Result { get; init; }
    };
}