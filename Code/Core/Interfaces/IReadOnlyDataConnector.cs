namespace Core;

public interface IReadOnlyDataConnector
{
    EquityPoint Read(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0);
    EquityPoint[] BulkRead(Equities equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0);
}
