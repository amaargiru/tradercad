// Read more at TraderCAD.com

using System;

namespace Core
{
    public record FinancePoint : IComparable
    {
        public DateTime PointDateTime { get; init; }
        public decimal Open { get; init; }
        public decimal High { get; init; }
        public decimal Low { get; init; }
        public decimal Close { get; init; }
        public decimal Average { get; init; }
        public decimal Volume { get; init; }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            FinancePoint other = (FinancePoint)obj;

            if (other != null)
                return this.PointDateTime.CompareTo(other.PointDateTime);
            else
                throw new ArgumentException("Object is not a FinancePoint");
        }
    };
}