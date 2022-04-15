namespace TraderCadCore;

public static partial class Utility
{
    public static decimal ConvertOneRangeToAnother(decimal value, decimal from1, decimal from2, decimal to1, decimal to2)
    {
        var OldRange = to1 - from1;
        var NewRange = to2 - from2;
        return ((value - from1) * NewRange / OldRange) + from2;
    }
}
