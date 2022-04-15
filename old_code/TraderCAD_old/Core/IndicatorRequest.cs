namespace TraderCadCore;

public record IndicatorRequest
{
    public IIndicator indicator { get; set; }
    public decimal[] coeffs { get; set; }
};
