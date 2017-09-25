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
        [RequiredField]
        public string Role { get; private set; }

        public Guid[] Permissions { get; private set; }

        public RoleAuthorizationCommand(string role, Guid[] permissions)
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
            var role = this.Roles.Include(e => e.RolePermissions).Find(r => r.Name == command.Role);

            if (role == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidRole));

            var recent = command.Permissions.ToList();
            var current = role.RolePermissions.Select(e => e.PermissionId).ToList();
            var deletion = current.Except(recent).ToList();
            var addition = recent.Except(current).ToList();

            role.RolePermissions.RemoveAll(e => deletion.Contains(e.PermissionId));

            var permissions = this.Permissions.Get(e => addition.Contains(e.Id));
            var additionPermissions = permissions.Select(permission => new RolePermission { PermissionId = permission.Id, RoleId = role.Id }).ToList();
            role.RolePermissions.AddRange(additionPermissions);

            this.Roles.Update(role);
            this.Roles.Commit();
            //this.Roles.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.RoleAuthorizationSuccess));
        }
    }
}
