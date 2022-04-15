// Read more at TraderCAD.com

namespace Core
{
    public enum UpdateDataConnectorResult
    {
        Ok,
        EquityNotExist,
        TimeframeNotExist,
        TimeoutExpired
    }
    public record UpdateDataConnectorAnswer
    {
        public string Equity { get; init; } = null!;
        public UpdateDataConnectorResult Result { get; init; }
    };
}