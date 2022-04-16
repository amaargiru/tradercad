namespace Core;

public record IndicatorPoint
{
    public DateTime? PointDateTime { get; set; }
    public decimal? Value { get; set; }
};
