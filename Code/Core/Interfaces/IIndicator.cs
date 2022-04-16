namespace Core;

public interface IIndicator
{
    IndicatorPoint[] Read(EquityPoint[] data, decimal[] coeffs);
}
