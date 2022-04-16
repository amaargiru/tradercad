namespace TraderCadCore;

public interface IStrategy
{
    List<AreaOfInterest> Read(EquityPoint[] data, params IndicatorRequest[] indicatorRequests);
}
