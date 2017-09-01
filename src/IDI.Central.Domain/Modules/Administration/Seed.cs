using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Domain.Modules.Administration
{
    public class ModuleCollection
    {
        public Module Administration { get; private set; }

        public Module Retailing { get; private set; }

        public ModuleCollection()
        {
            this.Administration = new Module { SN = 10, Name = "Administration", Code = "ADM", Description = "Administration", Icon = "fa fa-cogs" };
            this.Administration.NewPage(sn: 10, name: "Dashboard", controller: "platform", action: "dashboard", privilege: true, display: false);
            this.Administration.NewPage(sn: 20, name: "Settings", controller: "platform", action: "settings", privilege: true);
            this.Administration.NewPage(sn: 30, name: "Role", controller: "role", action: "administration", privilege: true);
            this.Administration.NewPage(sn: 40, name: "User", controller: "user", action: "administration", privilege: true);

            this.Retailing = new Module { SN = 20, Name = "Order", Code = "SMM", Description = "Sales", Icon = "fa fa-tasks" };
            this.Retailing.NewPage(sn: 10, name: "Order", controller: "order", action: "index", privilege: true);
        }
    }

    public class RoleCollection
    {
        public Role Administrators { get; private set; }

        public Role Staffs { get; private set; }

        public Role Customers { get; private set; }

        public RoleCollection()
        {
            this.Administrators = new Role { Name = Central.Common.Constants.Roles.Administrators, Descrition = "The administrator of system." };
            this.Staffs = new Role { Name = Central.Common.Constants.Roles.Staffs, Descrition = "The staff of system." };
            this.Customers = new Role { Name = Central.Common.Constants.Roles.Customers, Descrition = "The customer of system." };
        }
    }

    public class UserCollection
    {
        public User Administrator { get; private set; }

        public UserCollection()
        {
            string salt = Cryptography.Salt();

            this.Administrator = new User { UserName = "administrator", Salt = salt, Password = Cryptography.Encrypt("p@55w0rd", salt), Profile = new UserProfile { Name = "Administrator", Photo = "administrator.jpg" } };
        }
    }

    public class ClientCollection
    {
        public Client Central { get; private set; }

        public ClientCollection()
        {
            string salt = Cryptography.Salt();
            string clientId = "com.idi.central.web";

            this.Central = new Client { ClientId = clientId, SecretKey = Cryptography.Encrypt("6ED5C478-1F3A-4C82-B668-99917D67784E", salt), Salt = salt };
        }
    }

    public class ProductCollection
    {
        public List<Product> iPhones { get; private set; }

        public ProductCollection()
        {
            this.iPhones = new List<Product> {
                new Product { Name = "iPhone 5 黑色 16GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "16GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5 黑色 32GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5 黑色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5 白色 16GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "白色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "16GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5 白色 32GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "白色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5 白色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1442" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "白色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2012" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },

                new Product { Name = "iPhone 5s 深空灰色 32GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "深空灰色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5s 银色 32GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5s 金色 32GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "32GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5s 深空灰色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "深空灰色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5s 银色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 5s 金色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1533" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2013" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 6 深空灰色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1589" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "深空灰色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 6 银色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1589" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 6 金色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1589" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 6 Plus 深空灰色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1593" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "深空灰色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 6 Plus 银色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1593" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 6 Plus 金色 64GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1593" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2014" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "64GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 黑色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 亮黑色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "亮黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 金色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 玫瑰金色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "玫瑰金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 银色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 红色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1778" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "红色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 Plus 黑色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1661" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 Plus 亮黑色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1661" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "亮黑色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 Plus 金色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1661" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 Plus 玫瑰金色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1661" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "玫瑰金色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 Plus 银色 128GB", Tags = new List<TagModel>
                {
                    new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1661" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "银色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
                new Product { Name = "iPhone 7 Plus 红色 128GB", Tags = new List<TagModel>
                {
                   new TagModel { Key = Resources.Key.Tag.Model, Name="Model", Value = "A1661" },
                    new TagModel { Key = Resources.Key.Tag.Color, Name="Color", Value = "红色" },
                    new TagModel { Key = Resources.Key.Tag.Year, Name="Year", Value = "2016" },
                    new TagModel { Key = Resources.Key.Tag.Capacity, Name="Capacity", Value = "128GB" },
                }.ToJson() },
            };

            this.iPhones.ForEach(e => e.QRCode = Guid.NewGuid().AsCode());
        }
    }

    public class Seed
    {
        public ModuleCollection Modules { get; } = new ModuleCollection();

        public RoleCollection Roles { get; } = new RoleCollection();

        public UserCollection Users { get; } = new UserCollection();

        public ClientCollection Clients { get; } = new ClientCollection();

        public ProductCollection Products { get; } = new ProductCollection();

        public Seed()
        {
            //UserRoles
            this.Users.Administrator.Authorize(this.Roles.Administrators);

            //RoleModules
            this.Roles.Administrators.Authorize(this.Modules.Administration, this.Modules.Retailing);
            this.Roles.Staffs.Authorize(this.Modules.Retailing);
            //this.Roles.Customers.Authorize(this.Modules.Retailing);
        }
    }

    internal static class SeedDataExtension
    {
        public static Menu NewPage(this Module module, int sn, string name, string controller, string action, bool display = true, bool privilege = false, Menu parent = null)
        {
            var menu = new Menu
            {
                SN = sn,
                Code = parent == null ? $"{module.Code}{sn.ToString("D2")}" : $"{parent.Code}{sn.ToString("D2")}",
                Name = name,
                Controller = controller,
                Action = action,
                Display = display,
                Module = module,
                ParentId = parent == null ? Guid.Empty : parent.Id
            };

            module.Menus.Add(menu);

            if (privilege)
                module.NewPrivilege(menu);

            return menu;
        }

        public static Privilege NewPrivilege(this Module module, Menu menu, string name = null)
        {
            var privilege = new Privilege
            {
                Module = module,
                Name = string.IsNullOrEmpty(name) ? menu.Name : name,
                Code = menu.Code,
                PrivilegeType = PrivilegeType.View
            };

            module.Privileges.Add(privilege);

            return privilege;
        }

        public static void Authorize(this Role role, params Module[] modules)
        {
            role.RolePrivileges.Clear();
            modules.SelectMany(m => m.Privileges).ToList().ForEach(privilege =>
            {
                if (!role.RolePrivileges.Any(e => e.PrivilegeId == privilege.Id))
                    role.RolePrivileges.Add(new RolePrivilege { RoleId = role.Id, PrivilegeId = privilege.Id });
            });
        }

        public static void Authorize(this User user, params Role[] roles)
        {
            user.UserRoles.Clear();
            roles.ToList().ForEach(role =>
            {
                user.UserRoles.Add(new UserRole { User = user, Role = role });
            });
        }
    }
}
