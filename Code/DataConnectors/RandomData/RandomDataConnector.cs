using Core;

using static System.Math;

namespace DataConnectors;

public class RandomDataConnector : IReadOnlyDataConnector
{
    readonly decimal maxLevel = 1.5m;
    readonly decimal minLevel = .5m;
    readonly decimal deviation = .1m;
    readonly int precision = 5;

    public EquityPoint Read(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0)
    {
        var open = Round(Utility.GetRandomDecimal(minLevel, maxLevel), precision);
        var close = Round(Utility.GetRandomDecimal(minLevel, maxLevel), precision);
        var high = Round(Max(open, close) + Utility.GetRandomDecimal(0, deviation), precision);
        var low = Round(Min(open, close) - Utility.GetRandomDecimal(0, deviation), precision);

        return new EquityPoint
        {
            PointDateTime = dateTime,
            Open = open,
            Close = close,
            High = high,
            Low = low
        };
    }

    public EquityPoint[] BulkRead(Equities equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0)
    {
        if (startDateTime > endDateTime)
        {
            throw new ArgumentOutOfRangeException(
                $"{nameof(startDateTime)}({startDateTime}) is greater than {nameof(endDateTime)}({endDateTime})");
        }

        if (startDateTime == endDateTime)
        {
            return new EquityPoint[]
            {
                Read(equity, timeframe, startDateTime)
            };
        }

        var timeSpan = TimePeriod.Resolve(timeframe);
        var count = (int)Ceiling((endDateTime - startDateTime) / timeSpan) + 1;
        var randomData = new EquityPoint[count];

        for (int i = 0; i < count; i++)
        {
            var open = Round(Utility.GetRandomDecimal(minLevel, maxLevel), precision);
            var close = Round(Utility.GetRandomDecimal(minLevel, maxLevel), precision);
            var high = Round(Max(open, close) + Utility.GetRandomDecimal(0, deviation), precision);
            var low = Round(Min(open, close) - Utility.GetRandomDecimal(0, deviation), precision);

            randomData[i] = new EquityPoint
            {
                PointDateTime = startDateTime.Add(i * timeSpan),
                Open = open,
                High = high,
                Low = low,
                Close = close,
            };
        }

        return randomData;
    }
}
