namespace Core;

public record NeededIndicator
{
    public IIndicator? Indicator { get; set; }
    public decimal[]? Coeffs { get; set; }
};
