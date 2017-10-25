using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleMenuCommand : Command
    {
        [RequiredField]
        public string Role { get; set; }

        public List<int> Menus { get; set; }
    }

    public class RoleMenuCommandHandler : ICommandHandler<RoleMenuCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<Module> Modules { get; set; }

        public Result Execute(RoleMenuCommand command)
        {
            var role = Roles.Find(r => r.Name == command.Role);

            if (role == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidRole));

            var menus = Modules.Include(e => e.Menus).Get().Menus();

            role.Menus = menus.Intersect(command.Menus).ToJson();

            this.Roles.Update(role);
            this.Roles.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.RoleMenuAuthorizationSuccess));
        }
    }
}
