using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Domain.Modules.Retailing
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

    public class CustomerCollection
    {
        private readonly Random random = new Random();

        public List<Customer> Customers { get; private set; }

        public CustomerCollection()
        {
            this.Customers = new List<Customer>
            {
                new Customer { Name="李", Grade=0 },
                new Customer { Name="王", Grade=1 },
                new Customer { Name="张", Grade=2 },
                new Customer { Name="刘", Grade=3 },
                new Customer { Name="陈", Grade=4 },
                new Customer { Name="杨", Grade=5 },
                new Customer { Name="赵", Grade=6 },
                new Customer { Name="黄", Grade=7 },
                new Customer { Name="周", Grade=8 },
                new Customer { Name="吴", Grade=9 },
            };

            Update(this.Customers);
        }

        private void Update(List<Customer> customers)
        {
            for (var index = 0; index < customers.Count; index++)
            {
                var gender = (Gender)(index % 2);
                var name = string.Format("{0}{1}", customers[index].Name, gender == Gender.Male ? "先生" : "女士");
                var salt = Cryptography.Salt();
                var phone = $"{ 13900000000 + index}";

                customers[index].Name = name;
                customers[index].User = new User
                {
                    UserName = $"cust{phone}",
                    Salt = salt,
                    Password = Cryptography.Encrypt(phone.Substring(2, phone.Length - 3), salt),
                    IsLocked = true,
                    LockTime = DateTime.MaxValue,
                    Profile = new UserProfile
                    {
                        Name = name,
                        Gender = gender,
                        PhoneNum = phone,
                        Photo = $"{gender.ToString().ToLower()}.png"
                    }
                };
            }
        }
    }

    public class Seed
    {
        public ProductCollection Products { get; } = new ProductCollection();

        public CustomerCollection Customers { get; } = new CustomerCollection();

        public Seed() { }
    }
}
