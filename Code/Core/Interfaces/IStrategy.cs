namespace Core;

public interface IStrategy
{
    List<PointOfInterest> Read(EquityPoint[] data, params NeededIndicator[] indicatorRequests);
}
