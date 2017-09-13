using System;

namespace IDI.Central.Common.Enums
{
    [Flags]
    public enum PriceCategory : int
    {
        Cost = 2,
        Purchase = 4,
        Original = 8,
        Selling = 16,
        Discount = 32
    }
}
