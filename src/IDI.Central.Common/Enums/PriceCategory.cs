using System;
using System.ComponentModel;

namespace IDI.Central.Common
{
    [Flags]
    public enum PriceCategory
    {
        //[Description("成本价")]
        Cost = 2,
        //[Description("采购价")]
        Purchase = 4,
        //[Description("原价")]
        Original = 8,
        //[Description("现价")]
        Selling = 16,
        //[Description("折扣价")]
        Discount = 32,
        //[Description("会员价")]
        VIP = 64
    }
}
