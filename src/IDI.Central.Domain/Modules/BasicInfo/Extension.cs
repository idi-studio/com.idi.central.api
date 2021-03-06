﻿using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Models.BasicInfo;

namespace IDI.Central.Domain.Modules.BasicInfo
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

        public static decimal Available(this Product product)
        {
            if (product.Stocks == null || (product.Stocks != null && product.Stocks.Count == 0))
                return 0.00M;

            return product.Stocks.Sum(e => e.Available);
        }

        public static decimal Reserve(this Product product)
        {
            if (product.Stocks == null || (product.Stocks != null && product.Stocks.Count == 0))
                return 0.00M;

            return product.Stocks.Sum(e => e.Reserve);
        }

        public static decimal Quantity(this Product product)
        {
            if (product.Stocks == null || (product.Stocks != null && product.Stocks.Count == 0))
                return 0.00M;

            return product.Stocks.Sum(e => e.Quantity);
        }

        public static void In(this Store store, Product product, decimal qty, string bin, out List<StockTransaction> trans)
        {
            trans = new List<StockTransaction>();

            var stock = store.Stocks.FirstOrDefault(e => e.ProductId == product.Id && e.BinCode == bin);

            if (stock == null)
            {
                store.Stocks.Add(new Stock { BinCode = bin, ProductId = product.Id, Available = qty, StoreId = store.Id });
            }
            else
            {
                stock.Available += qty;
            }

            trans.Add(new StockTransaction { BinCode = bin, ProductId = product.Id, Quantity = qty, StoreId = store.Id, Category = StockTransactionType.StockIn });
        }

        public static bool Release(this Store store, Product product, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            return store.Stocks.Release(product.Stock.StoreId, product.Id, qty, bin, out remain, out trans);
        }

        public static bool Release(this Product product, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            return product.Stocks.Release(product.Stock.StoreId, product.Id, qty, bin, out remain, out trans);
        }

        private static bool Release(this List<Stock> stocks, Guid storeId, Guid productId, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            foreach (var stock in stocks)
            {
                if (stock.ProductId != productId)
                    continue;

                if (!bin.IsNull() && stock.BinCode != bin)
                    continue;

                var amount = stock.Reserve >= qty ? qty : stock.Reserve;

                stock.Reserve -= amount;
                stock.Available += amount;
                remain -= amount;

                trans.Add(new StockTransaction { BinCode = bin, ProductId = productId, Quantity = amount, StoreId = storeId, Category = StockTransactionType.Release });
            }

            return remain == 0;
        }

        public static bool Out(this Store store, Product product, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            foreach (var stock in store.Stocks)
            {
                if (stock.ProductId != product.Id)
                    continue;

                if (!bin.IsNull() && stock.BinCode != bin)
                    continue;

                var amount = stock.Reserve >= qty ? qty : stock.Reserve;

                stock.Reserve -= amount;
                remain -= amount;

                trans.Add(new StockTransaction { BinCode = bin, ProductId = product.Id, Quantity = amount, StoreId = store.Id, Category = StockTransactionType.StockOut });
            }

            return remain == 0;
        }

        public static bool Reserve(this Store store, Product product, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            return store.Stocks.Reserve(store.Id, product.Id, qty, bin, out remain, out trans);
        }

        public static bool Reserve(this Product product, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            return product.Stocks.Reserve(product.Stock.StoreId, product.Id, qty, bin, out remain, out trans);
        }

        private static bool Reserve(this List<Stock> stocks, Guid storeId, Guid productId, decimal qty, string bin, out decimal remain, out List<StockTransaction> trans)
        {
            remain = qty;
            trans = new List<StockTransaction>();

            foreach (var stock in stocks)
            {
                if (stock.ProductId != productId)
                    continue;

                if (!bin.IsNull() && stock.BinCode != bin)
                    continue;

                var amount = stock.Available >= qty ? qty : stock.Available;

                stock.Reserve += amount;
                stock.Available -= amount;
                remain -= amount;

                trans.Add(new StockTransaction { BinCode = bin, ProductId = productId, Quantity = amount, StoreId = storeId, Category = StockTransactionType.Reserve });
            }

            return remain == 0;
        }
    }
}
