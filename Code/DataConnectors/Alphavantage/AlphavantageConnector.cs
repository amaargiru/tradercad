using System.Globalization;

using Core;

using Newtonsoft.Json;

namespace DataConnectors;

public class AlphavantageConnector : IReadOnlyDataConnector
{
    public EquityPoint[] BulkRead(Equities equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0)
    {
        if (timeframe != Timeframe.D1)
        {
            throw new ArgumentOutOfRangeException(
                $"{nameof(AlphavantageConnector)} now supports only {nameof(Timeframe.D1)} timeframe, not {nameof(timeframe)}");
        }

        if (password is null)
        {
            throw new ArgumentNullException($"Alphavantage API requires an ApiKey, but {nameof(password)} is null");
        }

        string responseJson = AlphavantageJsonRequest.GetJson
            (
            function: "TIME_SERIES_DAILY",
            outputsize: "full",
            datatype: "json",
            symbol: Enum.GetName(equity),
            apiKey: password
            );

        AlphavantageStockResponse response = JsonConvert.DeserializeObject<AlphavantageStockResponse>(responseJson);

        var count = response.TimeSeriesDaily.Count;
        EquityPoint[] data = new EquityPoint[count];

        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var timeSpan = TimePeriod.Resolve(timeframe);
        var i = count--;

        foreach (var tick in response.TimeSeriesDaily)
        {
            data[i--] = new EquityPoint
            {
                PointDateTime = Utility.DateTimeFloor(DateTime.Parse(tick.Key), timeSpan),
                Open = decimal.Parse(tick.Value.Open),
                Close = decimal.Parse(tick.Value.Close),
                High = decimal.Parse(tick.Value.High),
                Low = decimal.Parse(tick.Value.Low),
            };
        }

        bool isMatchesDateTimeRange(DateTime dt) => (dt >= startDateTime) && (dt <= endDateTime);
        return data.Where(d => isMatchesDateTimeRange(d.PointDateTime)).ToArray();
    }

    [Obsolete($"Method \"{nameof(Read)}\" doesn't have built-in Alphavantage API support, please use \"{nameof(BulkRead)}\" instead.")]
    public EquityPoint Read(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0)
    {
        var data = BulkRead(equity, timeframe, DateTime.MinValue, DateTime.Now, password, timeout);

        foreach (var response in data)
        {
            if (response.PointDateTime == dateTime)
                return response;
        }

        return null;
    }
}
