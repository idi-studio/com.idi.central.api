using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleAuthorizationCommand : Command
    {
        [RequiredField]
        public string Role { get; private set; }

        public string[] Permissions { get; private set; }

        public RoleAuthorizationCommand(string role, string[] permissions)
        {
            this.Role = role;
            this.Permissions = permissions;
        }
    }

    public class RoleAuthorizationCommandHandler : ICommandHandler<RoleAuthorizationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<Permission> Permissions { get; set; }

        public Result Execute(RoleAuthorizationCommand command)
        {
            var role = Roles.Find(r => r.Name == command.Role);

            if (role == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidRole));

            var permissions = Permissions.Get(e => command.Permissions.Contains(e.Code)).ToArray();

            role.Authorize(permissions);

            this.Roles.Update(role);
            this.Roles.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.RoleAuthorizationSuccess));
        }
    }
}
