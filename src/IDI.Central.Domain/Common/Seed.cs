using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.BasicInfo;
using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure;

namespace IDI.Central.Domain.Common
{
    public class ModuleCollection
    {
        public Module Dashboard { get; private set; }

        public Module Administration { get; private set; }

        public Module BasicInfo { get; private set; }

        public Module Sales { get; private set; }

        public Module Inventory { get; private set; }

        public Module[] All { get { return new Module[] { this.Dashboard, this.Administration, this.BasicInfo, this.Sales, this.Inventory }; } }

        public Module[] None { get { return new Module[] { }; } }

        public ModuleCollection()
        {
            this.Dashboard = new Module
            {
                SN = 10,
                Name = "Dashboard",
                Route = "dashboard",
                Icon = "zmdi zmdi-view-dashboard"
            };

            this.Administration = new Module
            {
                SN = 20,
                Name = "Administration",
                Icon = "zmdi zmdi-accounts-list-alt",
                Menus = new List<Menu>
                {
                    new Menu { SN=2010, Name="Roles", Route="role/list"  },
                    new Menu { SN=2020, Name="Users", Route="user/list"  }
                }
            };

            this.BasicInfo = new Module
            {
                SN = 30,
                Name = "Basic Info",
                Icon = "zmdi zmdi-apps zmdi-hc-fw",
                Menus = new List<Menu>
                {
                    new Menu { SN=3010, Name="Products", Route="product/list"  }
                }
            };

            this.Sales = new Module
            {
                SN = 40,
                Name = "Sales",
                Icon = "zmdi zmdi-labels",
                Menus = new List<Menu>
                {
                    new Menu { SN=4010, Name="Orders", Route="order/list"  },
                    new Menu { SN=4020, Name="Customers", Route="cust/list"  },
                    new Menu { SN=4030, Name="Promotions", Route="prom/list"  }
                }
            };

            this.Inventory = new Module
            {
                SN = 50,
                Name = "Inventory",
                Icon = "zmdi zmdi-store",
                Menus = new List<Menu>
                {
                    new Menu { SN=5010, Name="Stores", Route="store/list"  }
                }
            };
        }
    }

    public class AuthorizationCollection
    {
        public List<Permission> Permissions { get; private set; }

        public AuthorizationCollection()
        {
            var authorization = Runtime.GetService<Core.Authentication.IAuthorization>();

            Permissions = authorization.Permissions.Select(p => new Permission
            {
                Code = p.Code,
                Module = p.Module,
                Type = p.Type,
                Name = p.Name
            }).ToList();
        }

        public Core.Authentication.IPermission[] GetPermissions(params string[] modules)
        {
            return Permissions.Where(e => modules.Contains(e.Module)).ToArray();
        }
    }

    public class RoleCollection
    {
        public Role Administrators { get; private set; }

        public Role Staffs { get; private set; }

        public Role Customers { get; private set; }

        public RoleCollection()
        {
            this.Administrators = new Role { Name = Configuration.Roles.Administrators, Descrition = "The administrator of system." };
            this.Staffs = new Role { Name = Configuration.Roles.Staffs, Descrition = "The staff of system." };
            this.Customers = new Role { Name = Configuration.Roles.Customers, Descrition = "The customer of system." };
        }
    }

    public class UserCollection
    {
        public User Administrator { get; private set; }

        public UserCollection()
        {
            string salt = Cryptography.Salt();

            this.Administrator = new User
            {
                UserName = "administrator",
                Salt = salt,
                Password = Cryptography.Encrypt("p@55w0rd", salt),
                Profile = new UserProfile { Name = "Administrator", Photo = "admin.jpg" },
            };
        }
    }

    public class ClientCollection
    {
        public Client Central { get; private set; }

        public Client Wechat { get; private set; }

        public ClientCollection()
        {
            string salt = Cryptography.Salt();

            this.Central = new Client { ClientId = Configuration.Clients.Central, SecretKey = Cryptography.Encrypt("6ED5C478-1F3A-4C82-B668-99917D67784E", salt), Salt = salt };
            this.Wechat = new Client { ClientId = Configuration.Clients.Wechat, SecretKey = Cryptography.Encrypt("BA65E9B3-93CF-407D-B2EF-5C291B9D3230", salt), Salt = salt };
        }
    }

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
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new Tag { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new Tag { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product
                {
                    Name = "iPhone 5s 金色 32GB",
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new Tag { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new Tag { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product {
                    Name = "iPhone 6 深空灰色 64GB",
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1589" },
                    new Tag { Key = Resources.Key.Tag.Color, Name="Color", Value = "深空灰色" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new Tag { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product {
                    Name = "iPhone 6 Plus 银色 64GB",
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1593" },
                    new Tag { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new Tag { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product {
                    Name = "iPhone 7 金色 128GB",
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new Tag { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new Tag { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },

            };

            this.Others = new List<Product> {
                new Product
                {
                    Name = "小米MIX 2 64GB",
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "MIX 2" },
                    new Tag { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2017" },
                    new Tag { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product
                {
                    Name = "iMac Retina 4K 21.5-inch",
                    Tags = new List<Tag> {
                    new Tag { Key = Resources.Key.Tag.Model, Name="Model", Value = "iMac" },
                    new Tag { Key = Resources.Key.Tag.Processor, Name="Processor", Value = "3.1 GHz Intel Core i5" },
                    new Tag { Key = Resources.Key.Tag.Year, Name="Year", Value = "2015" },
                    new Tag { Key = Resources.Key.Tag.Memory, Name="Memory", Value = "8G 1867 MHz DDR3" },
                    new Tag { Key = Resources.Key.Tag.Display, Name="Display", Value = "Retina 4K 21.5-inch" },
                    new Tag { Key = Resources.Key.Tag.Storage, Name="Storage", Value = "251GB" },
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
                    Password = Cryptography.Encrypt(phone.Fix(8), salt),
                    IsLocked = true,
                    LockTime = DateTime.MaxValue,
                    Profile = new UserProfile
                    {
                        Name = name,
                        Gender = gender,
                        PhoneNum = phone
                    }
                };

                var shipping = new ShippingAddress
                {
                    CustomerId = customers[index].Id,
                    Receiver = name,
                    ContactNo = phone,
                    Province = "四川",
                    City = "成都",
                    Area = "高新区",
                    Street = "天府四街",
                    Detail = "天府软件园C区",
                    Postcode = "610041"
                };

                customers[index].DefaultShippingId = shipping.Id;
                customers[index].Shippings.Add(shipping);
            }
        }
    }

    public class Seed
    {
        public AuthorizationCollection Authorization { get; } = new AuthorizationCollection();

        public ModuleCollection Modules { get; } = new ModuleCollection();

        public RoleCollection Roles { get; } = new RoleCollection();

        public UserCollection Users { get; } = new UserCollection();

        public ClientCollection Clients { get; } = new ClientCollection();

        public CustomerCollection Customers { get; } = new CustomerCollection();

        public ProductCollection Products { get; } = new ProductCollection();

        public Store Store = new Store { Name = "Main Warehouse" };

        public List<StockTransaction> StockTransactions { get; private set; } = new List<StockTransaction>();

        public Seed()
        {
            #region Administration
            this.Users.Administrator.Authorize(Roles.Administrators);

            this.Roles.Administrators.Authorize(Authorization.GetPermissions(Configuration.Modules.All));
            this.Roles.Staffs.Authorize(Authorization.GetPermissions(Configuration.Modules.Sales));
            this.Roles.Customers.Authorize(Authorization.GetPermissions(Configuration.Modules.Personal));

            this.Roles.Administrators.Authorize(Modules.All);
            this.Roles.Staffs.Authorize(Modules.BasicInfo, Modules.Sales, Modules.Inventory);
            this.Roles.Customers.Authorize(Modules.None);
            #endregion

            #region BasicInfo

            Products.iPhones.ForEach(e =>
            {
                var transactions = new List<StockTransaction>();
                e.Stock = new ProductStock { ProductId = e.Id, StoreId = this.Store.Id };
                this.Store.In(e, 100, Configuration.Inventory.DefaultBinCode, out transactions);
                this.StockTransactions.AddRange(transactions);
            });

            Products.Others.ForEach(e =>
            {
                var transactions = new List<StockTransaction>();
                e.Stock = new ProductStock { ProductId = e.Id, StoreId = this.Store.Id };
                this.Store.In(e, 50, Configuration.Inventory.DefaultBinCode, out transactions);
                this.StockTransactions.AddRange(transactions);
            });

            this.StockTransactions.ForEach(e => e.Category = StockTransactionType.Open);
            #endregion

            #region Sales
            Customers.Customers.ForEach(e => e.User.Authorize(new Role { Name = Configuration.Roles.Customers }));
            #endregion
        }
    }
}
