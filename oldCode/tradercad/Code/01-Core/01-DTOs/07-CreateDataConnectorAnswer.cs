// Read more at TraderCAD.com

namespace Core
{
    public enum CreateDataConnectorResult
    {
        Ok,
        DataAlreadyExist,
        TimeoutExpired
    }
    public record CreateDataConnectorAnswer
    {
        public string Equity { get; init; } = null!;
        public CreateDataConnectorResult Result { get; init; }
    };
}