using System;

namespace IDI.Central.Common
{
    [Flags]
    public enum PriceCategory : int
    {
        Cost = 2,
        Purchase = 4,
        Original = 8,
        Selling = 16,
        Discount = 32,
        VIP = 64
    }
}
