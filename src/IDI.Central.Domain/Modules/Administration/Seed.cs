using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;

namespace IDI.Central.Domain.Modules.Administration
{
    //public class ModuleCollection
    //{
    //    public Module Administration { get; private set; }

    //    public Module Sales { get; private set; }

    //    public Module Material { get; private set; }

    //    public ModuleCollection()
    //    {
    //        this.Administration = new Module { SN = 10, Name = "Administration", Code = "ADM", Description = "Administration", Icon = "fa fa-cogs" };
    //        this.Administration.NewPage(sn: 10, name: "Dashboard", controller: "platform", action: "dashboard", permission: true, display: false);
    //        this.Administration.NewPage(sn: 20, name: "Settings", controller: "platform", action: "settings", permission: true);
    //        this.Administration.NewPage(sn: 30, name: "Role", controller: "role", action: "administration", permission: true);
    //        this.Administration.NewPage(sn: 40, name: "User", controller: "user", action: "administration", permission: true);

    //        this.Sales = new Module { SN = 20, Name = "Order", Code = "SMM", Description = "Sales", Icon = "fa fa-tasks" };
    //        this.Sales.NewPage(sn: 10, name: "Order", controller: "order", action: "index", permission: true);
    //    }
    //}

    public class AuthorizationCollection
    {
        public List<Permission> Permissions { get; private set; }

        public AuthorizationCollection()
        {
            var authorization = new ApplicationAuthorization();
            Permissions = authorization.Permissions.Select(p => new Permission
            {
                Module = p.Module,
                Code = p.Code,
                Name = p.Name,
                Type = p.Type
            }).ToList();
        }

        public Permission[] GetPermissions(params string[] modules)
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
                Profile = new UserProfile { Name = "Administrator", Photo = "administrator.jpg" },
            };
        }
    }

    public class ClientCollection
    {
        public Client Central { get; private set; }

        public ClientCollection()
        {
            string salt = Cryptography.Salt();

            this.Central = new Client { ClientId = Configuration.Clients.Central, SecretKey = Cryptography.Encrypt("6ED5C478-1F3A-4C82-B668-99917D67784E", salt), Salt = salt };
        }
    }

    public class Seed
    {
        //public ModuleCollection Modules { get; } = new ModuleCollection();

        public AuthorizationCollection Authorization { get; } = new AuthorizationCollection();

        public RoleCollection Roles { get; } = new RoleCollection();

        public UserCollection Users { get; } = new UserCollection();

        public ClientCollection Clients { get; } = new ClientCollection();

        public Seed()
        {
            //UserRoles
            this.Users.Administrator.Authorize(Roles.Administrators);

            //RoleModules
            this.Roles.Administrators.Authorize(Authorization.GetPermissions(Configuration.Modules.All));
            this.Roles.Staffs.Authorize(Authorization.GetPermissions(Configuration.Modules.Sales));
        }
    }
}
