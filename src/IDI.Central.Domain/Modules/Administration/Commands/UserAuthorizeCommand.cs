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
    public class UserAuthorizeCommand : Command
    {
        [RequiredField]
        public string UserName { get; set; }

        public string[] Roles { get; set; }
    }

    public class UserAuthorizeCommandHandler : ICommandHandler<UserAuthorizeCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        public Result Execute(UserAuthorizeCommand command)
        {
            var user = Users.Include(e => e.Role).Find(e => e.UserName == command.UserName);

            if (user == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidUser));

            var roles = Roles.Get(e => command.Roles.Contains(e.Name)).ToArray();

            user.Authorize(roles);

            Users.Update(user);
            Users.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.AuthSuccess));
        }
    }
}
