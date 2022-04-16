namespace TraderCadCore;

public interface IFullAccessDataConnector
{
    EquityPoint Read(Equity equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0);
    EquityPoint[] BulkRead(Equity equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0);
    bool Insert(Equity equity, Timeframe timeframe, EquityPoint equityPoint, string password = null!, int timeout = 0);
    int BulkInsert(Equity equity, Timeframe timeframe, EquityPoint[] equityPoints, string password = null!, int timeout = 0);
    bool Delete(Equity equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0);
}
