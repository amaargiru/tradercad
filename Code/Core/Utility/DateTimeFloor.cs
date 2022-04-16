namespace TraderCadCore;

public static partial class Utility
{
    public static DateTime DateTimeFloor(DateTime dateTime, TimeSpan span)
    {
        var ticks = dateTime.Ticks / span.Ticks;
        return new DateTime(ticks * span.Ticks);
    }
}
