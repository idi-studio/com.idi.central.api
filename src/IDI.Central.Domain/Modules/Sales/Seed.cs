using System;
using System.Collections.Generic;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;

namespace IDI.Central.Domain.Modules.Sales
{
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
        public CustomerCollection Customers { get; } = new CustomerCollection();

        public Seed() { }
    }
}
