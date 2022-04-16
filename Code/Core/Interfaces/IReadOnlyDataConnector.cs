namespace TraderCadCore;

public interface IReadOnlyDataConnector
{
    EquityPoint Read(Equity equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0);
    EquityPoint[] BulkRead(Equity equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0);
}
