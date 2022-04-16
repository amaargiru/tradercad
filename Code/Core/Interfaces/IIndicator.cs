namespace TraderCadCore;

public interface IIndicator
{
    IndicatorPoint[] Read(EquityPoint[] data, decimal[] coeffs);
}
