namespace TraderCadCore;

public static partial class Utility
{
    public static decimal CalculateAverage(EquityPoint data) => (data.Open + data.High + data.Low + data.Close) / 4;
    public static decimal[] CalculateAverage(EquityPoint[] data)
    {
        var dataLength = data.Length;
        var averages = new decimal[dataLength];

        for (var i = 0; i < dataLength; i++)
        {
            averages[i] = (data[i].Open + data[i].High + data[i].Low + data[i].Close) / 4;
        }

        return averages;
    }
}
