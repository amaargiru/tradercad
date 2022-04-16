namespace TraderCadCore;

public class Crossover : IStrategy
{
    public List<AreaOfInterest> Read(EquityPoint[] data, params IndicatorRequest[] indicatorRequests)
    {
        var diff = new List<IndicatorPoint>();

        var indicator = indicatorRequests[0].indicator; // Only one indicator is needed for the Crossover strategy
        var answer = indicator.Read(data, indicatorRequests[0].coeffs);

        foreach (var d in data)
        {
            var indicatorPoint = answer.SingleOrDefault(a => a.PointDateTime == d.PointDateTime);
            if (indicatorPoint is not null)
            {
                diff.Add(new IndicatorPoint { PointDateTime = d.PointDateTime, Value = Utility.CalculateAverage(d) - indicatorPoint.Value });
            }
        }

        var areas = new List<AreaOfInterest>();

        var diffCount = diff.Count;

        if (diffCount >= 3)
        {
            for (var i = 1; i < diffCount; i++)
            {
                if ((diff[i].Value > 0 && diff[i - 1].Value <= 0) || (diff[i].Value < 0 && diff[i - 1].Value >= 0))
                {
                    areas.Add(new AreaOfInterest
                    {
                        Message = "Crossover",
                        AreaDateTime = (DateTime)diff[i].PointDateTime,
                        AreaValue = (decimal)diff[i].Value
                    });
                }
            }
        }

        return areas;
    }
}
