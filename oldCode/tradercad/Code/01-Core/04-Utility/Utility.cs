// Read more at TraderCAD.com

using System;
using System.IO;
using System.Security.Cryptography;

namespace Core
{
    public class Utility
    {
        public static decimal GetRandomDecimal(decimal min, decimal max)
        {
            using var rng = new RNGCryptoServiceProvider();
            if (min >= max)
                throw new ArgumentOutOfRangeException
                (
                    nameof(min) + " or " + nameof(max),
                    "DateTime parameters are wrong."
                );

            var data = new byte[4];

            rng.GetBytes(data);
            Random random = new(BitConverter.ToInt32(data, 0));
            return (decimal) random.NextDouble() * (max - min) + min;
        }

        public static T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (var i = 0; i < length; ++i) array[i] = new T();

            return array;
        }

        public static DateTime Floor(DateTime dateTime, TimeSpan span)
        {
            var ticks = dateTime.Ticks / span.Ticks;
            return new DateTime(ticks * span.Ticks);
        }

        public static void CheckRequestEquityForInvalidPathChars(string equity)
        {
            var invalidPathChars = Path.GetInvalidPathChars();
            var detectedInvalidPathChars = equity.IndexOfAny(invalidPathChars);

            if (detectedInvalidPathChars > 0)
                throw new ArgumentOutOfRangeException
                (
                    equity + " contains invalid path chars " + ".",
                    "\"" + nameof(equity) + "\" parameter are wrong."
                );
        }
    }
}