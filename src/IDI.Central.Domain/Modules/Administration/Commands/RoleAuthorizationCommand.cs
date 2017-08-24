using System;
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
        [RequiredField(Resources.Key.DisplayName.Role)]
        public string RoleName { get; private set; }

        public Guid[] Privileges { get; private set; }

        public RoleAuthorizationCommand(string rolename, Guid[] privileges)
        {
            this.RoleName = rolename;
            this.Privileges = privileges;
        }
    }

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
            var role = this.Roles.Include(e => e.RolePrivileges).Find(r => r.Name == command.RoleName);

            if (role == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidRole));

            var recent = command.Privileges.ToList();
            var current = role.RolePrivileges.Select(e => e.PrivilegeId).ToList();
            var deletion = current.Except(recent).ToList();
            var addition = recent.Except(current).ToList();

            role.RolePrivileges.RemoveAll(e => deletion.Contains(e.PrivilegeId));

            var privileges = this.Privileges.Get(e => addition.Contains(e.Id));
            var additionPrivileges = privileges.Select(privilege => new RolePrivilege { PrivilegeId = privilege.Id, RoleId = role.Id }).ToList();
            role.RolePrivileges.AddRange(additionPrivileges);

            this.Roles.Update(role);
            this.Roles.Commit();
            //this.Roles.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.RoleAuthorizationSuccess));
        }
    }
}
