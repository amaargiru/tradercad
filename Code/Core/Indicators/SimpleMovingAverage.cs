namespace Core;

public class SimpleMovingAverage : IIndicator
{
    public Point[] Read(EquityPoint[] data, decimal[] coeffs)
    {
        if (coeffs is null)
        {
            throw new ArgumentNullException(nameof(coeffs), $"SMA indicator requires number of time periods, " +
                $"but {nameof(coeffs)} is null.");
        }
        else if (coeffs.Min() <= 0)
        {
            throw new ArgumentNullException($"Number of time periods for SMA indicator must be greater than zero, " +
                $"but {nameof(coeffs)} contains {coeffs.Min()}.");
        }

        var dataLength = data.Length;
        var period = coeffs[0];
        var averages = Utility.CalculateAverage(data);

        Point[] sma = Utility.InitializeArray<Point>(dataLength);

        decimal window = 0;

        for (int i = 0; i < dataLength; ++i)
        {
            window += averages[i]; // Add the latest entry to the window

            if (i - period + 1 >= 0)
            {
                sma[i].PointDateTime = data[i].PointDateTime;
                sma[i].Value = window / period;

                window -= averages[i - (int)period + 1]; // Drop off the older entry
            }
        }

        return sma;
    }
}
