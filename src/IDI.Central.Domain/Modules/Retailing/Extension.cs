using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;

namespace IDI.Central.Domain.Modules.Retailing
{
    internal static class Extension
    {
        public static bool Valid(this Product product)
        {
            if (product == null)
                return false;

            return product.Enabled && product.OnShelf;
        }

        public static IDictionary<string, decimal> SellingPrices(this Product product)
        {
            if (product == null)
                return new Dictionary<string, decimal>();

            var current = DateTime.Now;
            var category = PriceCategory.Discount | PriceCategory.Selling | PriceCategory.VIP;

            var prices = product.Prices.Where(e => e.Enabled && e.StartDate <= current && e.DueDate >= current && category.HasFlag(e.Category));

            return prices.ToDictionary(e => e.Category == PriceCategory.VIP ? $"{e.Category}-{e.Grade}" : $"{e.Category}", e => e.Amount);
        }

        public static bool AllowModifyItem(this Order order)
        {
            if (order == null)
                return false;

            if (order.Status == OrderStatus.Pending)
                return true;

            return false;
        }
    }
}
