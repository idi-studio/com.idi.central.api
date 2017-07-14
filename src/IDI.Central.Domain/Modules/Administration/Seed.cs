using System;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;

namespace IDI.Central.Domain.Modules.Administration
{
    public class ModuleCollection
    {
        public Module Administration { get; private set; }

        public Module Sales { get; private set; }

        public ModuleCollection()
        {
            this.Administration = new Module { SN = 10, Name = "Administration", Code = "ADM", Description = "Administration", Icon = "fa fa-cogs" };
            this.Administration.NewPage(sn: 10, name: "Dashboard", controller: "platform", action: "dashboard", privilege: true, display: false);
            this.Administration.NewPage(sn: 20, name: "Settings", controller: "platform", action: "settings", privilege: true);
            this.Administration.NewPage(sn: 30, name: "Role", controller: "role", action: "administration", privilege: true);
            this.Administration.NewPage(sn: 40, name: "User", controller: "user", action: "administration", privilege: true);

            this.Sales = new Module { SN = 20, Name = "Order", Code = "SMM", Description = "Sales", Icon = "fa fa-tasks" };
            this.Sales.NewPage(sn: 10, name: "Order", controller: "order", action: "index", privilege: true);
        }
    }

    public class RoleCollection
    {
        public Role Administrators { get; private set; }

        public RoleCollection()
        {
            this.Administrators = new Role { Name = "Administrators" };
        }
    }

    public class UserCollection
    {
        public User Administrator { get; private set; }

        public UserCollection()
        {
            string salt = Cryptography.Salt();

            this.Administrator = new User { UserName = "administrator", Salt = salt, Password = Cryptography.Encrypt("p@55w0rd", salt), Profile = new UserProfile { Name = "Administrator" } };
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
