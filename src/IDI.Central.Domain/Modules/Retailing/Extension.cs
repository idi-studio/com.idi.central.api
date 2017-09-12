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

        public static PriceModel FavorablePrice(this Product product, int custGrade)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (product.Prices.Count == 0)
                return null;

            var current = DateTime.Now;
            var category = PriceCategory.Discount | PriceCategory.Selling;

            var prices = product.Prices.Where(e => e.Enabled && e.PeriodStart <= current && e.PeriodEnd >= current && category.HasFlag(e.Category));

            var list = new List<PriceModel>();

            foreach (var price in prices)
            {
                var temp = Enumerable.Range(price.GradeFrom, price.GradeTo - price.GradeFrom + 1).Select(grade => new PriceModel
                {
                    Id = price.Id,
                    Category = price.Category,
                    Amount = price.Amount,
                    Grade = grade
                }).Where(e => e.Grade <= custGrade);

                list.AddRange(temp);
            }

            return list.OrderBy(e => e.Amount).FirstOrDefault();
        }

        public static bool AllowModifyItem(this Order order)
        {
            if (order == null)
                return false;

            if (order.Status == OrderStatus.Pending)
                return true;

            return false;
        }

        public static bool HasItems(this Order order)
        {
            if (order == null)
                return false;

            return order.Items.Count > 0;
        }

        public static bool HasCustomer(this Order order)
        {
            if (order == null)
                return false;

            return order.CustomerId.HasValue && order.Customer != null;
        }
    }
}
