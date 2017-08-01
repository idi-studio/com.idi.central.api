using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleAuthorizationCommandHandler : ICommandHandler<RoleAuthorizationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<Privilege> Privileges { get; set; }

        public Result Execute(RoleAuthorizationCommand command)
        {
            var role = this.Roles.Find(r => r.Name == command.RoleName, r => r.RolePrivileges);

            if (role == null)
                return Result.Fail(Localization.Get( Resources.Key.Command.InvalidRole));

            var recent = command.Privileges.ToList();
            var current = role.RolePrivileges.Select(e => e.PrivilegeId).ToList();
            var deletion = current.Except(recent).ToList();
            var addition = recent.Except(current).ToList();

            role.RolePrivileges.RemoveAll(e => deletion.Contains(e.PrivilegeId));

            var privileges = this.Privileges.Get(e => addition.Contains(e.Id));
            var additionPrivileges = privileges.Select(privilege => new RolePrivilege { PrivilegeId = privilege.Id, RoleId = role.Id }).ToList();
            role.RolePrivileges.AddRange(additionPrivileges);

            this.Roles.Update(role);
            this.Roles.Context.Commit();
            this.Roles.Context.Dispose();

            return Result.Success(message: Localization.Get( Resources.Key.Command.RoleAuthorizationSuccess));
        }
    }
}
