namespace Core;

public class Crossover : IStrategy
{
    public List<PointOfInterest> Read(EquityPoint[] data, params NeededIndicator[] indicatorRequests)
    {
        var indicator = indicatorRequests[0].Indicator; // Only one indicator is needed for the Crossover strategy
        var coeffs = indicatorRequests[0].Coeffs;

        if (indicator is null) throw new ArgumentNullException(nameof(indicator) ,"Indicator is null.");
        if (coeffs is null) throw new ArgumentNullException(nameof(coeffs), "Coefficients' array is null.");

        var indicatorResult = indicator.Read(data, coeffs);

        var diff = new List<IndicatorPoint>();

        foreach (var d in data)
        {
            var indicatorPoint = indicatorResult.SingleOrDefault(a => a.PointDateTime == d.PointDateTime);
            if (indicatorPoint is not null)
            {
                diff.Add(new IndicatorPoint { PointDateTime = d.PointDateTime, Value = Utility.CalculateAverage(d) - indicatorPoint.Value });
            }
        }

        var areaOfInterest = new List<PointOfInterest>();

        var diffCount = diff.Count;

        if (diffCount >= 3)
        {
            for (var i = 1; i < diffCount; i++)
            {
                if ((diff[i].Value > 0 && diff[i - 1].Value <= 0) || (diff[i].Value < 0 && diff[i - 1].Value >= 0))
                {
                    areaOfInterest.Add(new PointOfInterest
                    {
                        Message = "Crossover",
                        AreaDateTime = (DateTime)diff[i].PointDateTime,
                        AreaValue = (decimal)diff[i].Value
                    });
                }
            }
        }

        return areaOfInterest;
    }
}
