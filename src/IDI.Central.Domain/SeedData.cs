using System;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;

namespace IDI.Central.Domain
{
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

    public sealed class SeedData
    {
        public class ModuleSeed
        {
            public Module Administration { get; private set; }

            public Module OMM { get; private set; }

            public ModuleSeed()
            {
                this.Administration = new Module { SN = 10, Name = "Platform", Code = "Administration", Description = "Platform Management", Icon = "fa fa-cogs" };
                this.OMM = new Module { SN = 20, Name = "Order", Code = "OMM", Description = "Order Management", Icon = "fa fa-tasks" };
            }
        }

        public class RoleSeed
        {
            public Role Administrators { get; private set; }

            public RoleSeed()
            {
                this.Administrators = new Role { Name = "Administrators" };
            }
        }

        public class UserSeed
        {
            public User Administrator { get; private set; }

            public UserSeed()
            {
                this.Administrator = new User { UserName = "administrator", Salt = Cryptography.Salt()  };
                this.Administrator.Password = Cryptography.Encrypt("p@55w0rd", this.Administrator.Salt);
                this.Administrator.Profile = new UserProfile { Name = "Administrator" };
            }
        }

        public ModuleSeed Modules { get; private set; }

        public RoleSeed Roles { get; private set; }

        public UserSeed Users { get; private set; }

        public SeedData()
        {
            this.Modules = new ModuleSeed();
            this.Roles = new RoleSeed();
            this.Users = new UserSeed();

            Initialize();
        }

        private void Initialize()
        {
            this.Modules.Administration.NewPage(sn: 10, name: "Dashboard", controller: "platform", action: "dashboard", privilege: true, display: false);
            this.Modules.Administration.NewPage(sn: 20, name: "Settings", controller: "platform", action: "settings", privilege: true);
            this.Modules.Administration.NewPage(sn: 30, name: "Role", controller: "role", action: "administration", privilege: true);
            this.Modules.Administration.NewPage(sn: 40, name: "User", controller: "user", action: "administration", privilege: true);

            this.Modules.OMM.NewPage(sn: 10, name: "Order", controller: "order", action: "index", privilege: true);

            this.Users.Administrator.Authorize(this.Roles.Administrators);
            this.Roles.Administrators.Authorize(this.Modules.Administration, this.Modules.OMM);
        }
    }
}