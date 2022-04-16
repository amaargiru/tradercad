namespace Core;

public interface IIndicator
{
    Point[] Read(EquityPoint[] data, decimal[] coeffs);
}
