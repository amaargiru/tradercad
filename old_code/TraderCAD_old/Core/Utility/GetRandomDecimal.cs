using System.Security.Cryptography;

namespace TraderCadCore;

public static partial class Utility
{
    public static decimal GetRandomDecimal(decimal min, decimal max)
    {
        if (min >= max)
        {
            throw new ArgumentOutOfRangeException($"{nameof(min)}({min}) >= {nameof(max)}({max})");
        }

        var randomData = RandomNumberGenerator.GetBytes(4);
        Random random = new(BitConverter.ToInt32(randomData, 0));
        return (decimal)random.NextDouble() * (max - min) + min;
    }
}
