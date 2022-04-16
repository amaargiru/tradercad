namespace Core;

public static partial class Utility
{
    public static decimal ConvertOneRangeToAnother(decimal value, decimal from1, decimal from2, decimal to1, decimal to2)
    {
        if (from1 == to1)
            throw new ArgumentOutOfRangeException($"Wrong arguments: {nameof(from1)} and {nameof(to1)} = {from1}");

        if (from2 == to2)
            throw new ArgumentOutOfRangeException($"Wrong arguments: {nameof(from2)} and {nameof(to2)} = {from2}");

        var OldRange = to1 - from1;
        var NewRange = to2 - from2;
        return ((value - from1) * NewRange / OldRange) + from2;
    }
}
