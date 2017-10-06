using System.Collections.Generic;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Infrastructure;

namespace IDI.Central.Domain.Modules.Administration
{
    public class AuthorizationCollection
    {
        public List<AggregateRoots.Permission> Permissions { get; private set; }

        public AuthorizationCollection()
        {
            var authorization = Runtime.GetService<IAuthorization>();

            Permissions = authorization.Permissions.Select(p => new AggregateRoots.Permission
            {
                Code = p.Code,
                Module = p.Module,
                Type = p.Type,
                Name = p.Name
            }).ToList();
        }

        public IPermission[] GetPermissions(params string[] modules)
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

        public ClientCollection()
        {
            string salt = Cryptography.Salt();

            this.Central = new Client { ClientId = Configuration.Clients.Central, SecretKey = Cryptography.Encrypt("6ED5C478-1F3A-4C82-B668-99917D67784E", salt), Salt = salt };
        }
    }

    public class Seed
    {
        public AuthorizationCollection Authorization { get; } = new AuthorizationCollection();

        public RoleCollection Roles { get; } = new RoleCollection();

        public UserCollection Users { get; } = new UserCollection();

        public ClientCollection Clients { get; } = new ClientCollection();

        public Seed()
        {
            this.Users.Administrator.Authorize(Roles.Administrators);

            this.Roles.Administrators.Authorize(Authorization.GetPermissions(Configuration.Modules.All));
            this.Roles.Staffs.Authorize(Authorization.GetPermissions(Configuration.Modules.Sales));
            this.Roles.Customers.Authorize(Authorization.GetPermissions(Configuration.Modules.Personal));
        }
    }
}
