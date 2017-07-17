using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserAuthenticationCommandHandler : ICommandHandler<UserAuthenticationCommand>
    {
        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(UserAuthenticationCommand command)
        {
            var user = this.Users.Find(u => u.UserName == command.UserName);

            if (user == null)
                return Result.Fail(Language.Instance.GetByCulture("command", "invalid-username-or-password"));

            string hashed = Cryptography.Encrypt(command.Password, user.Salt);

            if (user.Password != hashed)
                return Result.Fail(Language.Instance.GetByCulture("command", "invalid-username-or-password"));

            return Result.Success(message: Language.Instance.GetByCulture("command", "authentication-success"));
        }
    }
}
