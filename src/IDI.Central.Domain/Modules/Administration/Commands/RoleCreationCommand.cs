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
    public class RoleCreationCommand : Command
    {
        [RequiredField(Resources.Key.DisplayName.Role)]
        [StringLength(Resources.Key.DisplayName.Role, MaxLength = 20)]
        public string RoleName { get; private set; }

        public RoleCreationCommand(string rolename)
        {
            this.RoleName = rolename;
        }
    }

    public class RoleCreationCommandHandler : ICommandHandler<RoleCreationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        public Result Execute(RoleCreationCommand command)
        {
            if (this.Roles.Exist(e => e.Name == command.RoleName))
                return Result.Fail(Localization.Get(Resources.Key.Command.RoleExists));

            var role = new Role { Name = command.RoleName };

            this.Roles.Add(role);
            this.Roles.Context.Commit();
            this.Roles.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreationSuccess));
        }
    }
}
