using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleCreationCommandHandler : ICommandHandler<RoleCreationCommand>
    {
        [Injection]
        public IRepository<Role> Roles { get; set; }

        public Result Execute(RoleCreationCommand command)
        {

            if (this.Roles.Exist(e => e.Name == command.RoleName))
                return Result.Fail(Language.Instance.GetByCulture(Resources.Prefix.COMMAND, Resources.Key.ROLE_EXISTS));

            var role = new Role { Name = command.RoleName };

            this.Roles.Add(role);
            this.Roles.Context.Commit();
            this.Roles.Context.Dispose();

            return Result.Success(message: Language.Instance.GetByCulture(Resources.Prefix.COMMAND, Resources.Key.CREATION_SUCCESS));
        }
    }
}
