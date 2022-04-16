﻿namespace Core;

public record EquityPoint
{
    public DateTime PointDateTime
    {
        get; set;
    }
    public decimal Open
    {
        get; set;
    }
    public decimal High
    {
        get; set;
    }
    public decimal Low
    {
        get; set;
    }
    public decimal Close
    {
        get; set;
    }
};
