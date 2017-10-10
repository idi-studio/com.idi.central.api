using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Domain.Modules.BasicInfo
{
    public class ProductCollection
    {
        private readonly Random random = new Random();
        private readonly DateTime today = DateTime.Now.Date;

        public List<Product> iPhones { get; private set; }

        public List<Product> Others { get; private set; }

        public ProductCollection()
        {
            this.iPhones = new List<Product> {
                new Product
                {
                    Name = "iPhone 5 黑色 32GB",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product
                {
                    Name = "iPhone 5s 金色 32GB",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product {
                    Name = "iPhone 6 深空灰色 64GB",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1589" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "深空灰色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product {
                    Name = "iPhone 6 Plus 银色 64GB",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1593" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product {
                    Name = "iPhone 7 金色 128GB",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },

            };

            this.Others = new List<Product> {
                new Product
                {
                    Name = "小米MIX 2 64GB",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "MIX 2" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2017" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product
                {
                    Name = "iMac Retina 4K 21.5-inch",
                    Tags = new List<TagModel> {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "iMac" },
                    new TagModel { Key = Resources.Key.Tag.Processor, Name="Processor", Value = "3.1 GHz Intel Core i5" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2015" },
                    new TagModel { Key = Resources.Key.Tag.Memory, Name="Memory", Value = "8G 1867 MHz DDR3" },
                    new TagModel { Key = Resources.Key.Tag.Display, Name="Display", Value = "Retina 4K 21.5-inch" },
                    new TagModel { Key = Resources.Key.Tag.Storage, Name="Storage", Value = "251GB" },
                }.ToJson() },
            };

            Update(this.iPhones);
            Update(this.Others);
        }

        private void Update(List<Product> products)
        {
            var selling = 1000M;
            var start = new DateTime(today.Year, 1, 1, 0, 0, 0);
            var end = new DateTime(2099, 12, 31, 23, 59, 59);
            var beginOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0);
            var endOfMonth = beginOfMonth.AddMonths(1).AddSeconds(-1);

            for (var index = 0; index < products.Count; index++)
            {
                var pid = products[index].Id;

                products[index].OnShelf = true;
                products[index].QRCode = Guid.NewGuid().AsCode();
                products[index].Prices.Add(new ProductPrice
                {
                    Category = PriceCategory.Selling,
                    Amount = selling,
                    PeriodStart = start,
                    PeriodEnd = end,
                    ProductId = pid,
                    Enabled = true
                });

                var discounts = Enumerable.Range(1, 9).Select(grade => new ProductPrice
                {
                    Category = PriceCategory.Discount,
                    Amount = selling * (10M - grade) / 10M,
                    GradeFrom = grade,
                    GradeTo = grade,
                    PeriodStart = beginOfMonth,
                    PeriodEnd = endOfMonth,
                    ProductId = pid,
                    Enabled = true
                });

                products[index].Prices.AddRange(discounts);

                selling += 50;
            }
        }
    }

    public class Seed
    {
        public ProductCollection Products { get; } = new ProductCollection();

        public Store Store  = new Store { Name = "Main" };

        public Seed()
        {
            Products.iPhones.ForEach(e => this.Store.Inbound(e, 100));
            Products.Others.ForEach(e => this.Store.Inbound(e, 50));
        }
    }
}
