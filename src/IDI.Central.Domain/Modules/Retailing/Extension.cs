using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;

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

        public static List<PriceModel> PriceList(this Product product)
        {
            var list = new List<PriceModel>();

            if (product == null)
                return list;

            var current = DateTime.Now;
            var category = PriceCategory.Discount | PriceCategory.Selling;

            var prices = product.Prices.Where(e => e.Enabled && e.PeriodStart <= current && e.PeriodEnd >= current && category.HasFlag(e.Category));

            foreach (var price in prices)
            {
                list.AddRange(Enumerable.Range(price.GradeFrom, price.GradeTo - price.GradeFrom + 1)
                    .Select(grade => new PriceModel
                    {
                        Category = price.Category,
                        Amount = price.Amount,
                        Grade = grade
                    }));
            }

            return list;
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
