namespace Core;

public record PointOfInterest
{
    public decimal AreaValue
    {
        get; set;
    }
    public DateTime AreaDateTime
    {
        get; set;
    }
    public string? Message
    {
        get; set;
    }
};
