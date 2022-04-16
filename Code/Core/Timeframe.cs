using System;

namespace Core;

// List of all available timeframes
public enum Timeframe
{
    D1
}

// All available and tested timeframes are bounded to the enum Timeframe, so that the developer is not forced
// to insert a non-existent timeframe when calling methods, for example TimeSpan(days: 0, 0, 15, 0)
public static class TimePeriod
{
    public static TimeSpan Resolve(Timeframe timeframe)
    {
        return timeframe switch
        {
            Timeframe.D1 => new TimeSpan(days: 1, 0, 0, 0),
            _ => throw new ArgumentException($"Timeframe \"{timeframe}\" isn't resolving")
        };
    }
}
