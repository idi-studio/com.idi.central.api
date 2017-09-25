using System;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;

namespace IDI.Central.Domain.Modules.Administration
{
    public class ModuleCollection
    {
        public Module Administration { get; private set; }

        public Module Sales { get; private set; }

        public Module Material { get; private set; }

        public ModuleCollection()
        {
            this.Administration = new Module { SN = 10, Name = "Administration", Code = "ADM", Description = "Administration", Icon = "fa fa-cogs" };
            this.Administration.NewPage(sn: 10, name: "Dashboard", controller: "platform", action: "dashboard", permission: true, display: false);
            this.Administration.NewPage(sn: 20, name: "Settings", controller: "platform", action: "settings", permission: true);
            this.Administration.NewPage(sn: 30, name: "Role", controller: "role", action: "administration", permission: true);
            this.Administration.NewPage(sn: 40, name: "User", controller: "user", action: "administration", permission: true);

            this.Sales = new Module { SN = 20, Name = "Order", Code = "SMM", Description = "Sales", Icon = "fa fa-tasks" };
            this.Sales.NewPage(sn: 10, name: "Order", controller: "order", action: "index", permission: true);
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

    public class Seed
    {
        public ModuleCollection Modules { get; } = new ModuleCollection();

        public RoleCollection Roles { get; } = new RoleCollection();

        public UserCollection Users { get; } = new UserCollection();

        public ClientCollection Clients { get; } = new ClientCollection();

        public Seed()
        {
            //UserRoles
            this.Users.Administrator.Authorize(this.Roles.Administrators);

            //RoleModules
            this.Roles.Administrators.Authorize(this.Modules.Administration, this.Modules.Sales);
            this.Roles.Staffs.Authorize(this.Modules.Sales);
        }
    }

    internal static class SeedDataExtension
    {
        public static Menu NewPage(this Module module, int sn, string name, string controller, string action, bool display = true, bool permission = false, Menu parent = null)
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

            if (permission)
                module.NewPermission(menu);

            return menu;
        }

        public static Permission NewPermission(this Module module, Menu menu, string name = null)
        {
            var permission = new Permission
            {
                Module = module,
                Name = string.IsNullOrEmpty(name) ? menu.Name : name,
                Code = menu.Code,
                PermissionType = PermissionType.View
            };

            module.Permissions.Add(permission);

            return permission;
        }

        public static void Authorize(this Role role, params Module[] modules)
        {
            role.RolePermissions.Clear();
            modules.SelectMany(m => m.Permissions).ToList().ForEach(permission =>
            {
                if (!role.RolePermissions.Any(e => e.PermissionId == permission.Id))
                    role.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = permission.Id });
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
