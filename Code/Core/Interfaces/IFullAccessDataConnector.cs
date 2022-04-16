namespace Core;

public interface IFullAccessDataConnector
{
    EquityPoint Read(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0);
    EquityPoint[] BulkRead(Equities equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0);
    bool Insert(Equities equity, Timeframe timeframe, EquityPoint equityPoint, string password = null!, int timeout = 0);
    int BulkInsert(Equities equity, Timeframe timeframe, EquityPoint[] equityPoints, string password = null!, int timeout = 0);
    bool Delete(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0);
}
