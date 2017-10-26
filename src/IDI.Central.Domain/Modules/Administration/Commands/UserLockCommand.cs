using System;
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
    public class UserLockCommand : Command
    {
        [RequiredField]
        [StringLength(MaxLength = 20)]
        public string UserName { get; private set; }

        public UserLockCommand(string username)
        {
            this.UserName = username;
        }
    }

    public class UserLockCommandHandler : ICommandHandler<UserLockCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(UserLockCommand command)
        {
            var user = this.Users.Find(e => e.UserName == command.UserName);

            if (user == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            user.LockTime = user.IsLocked ? new DateTime?() : DateTime.MaxValue;
            user.IsLocked = !user.IsLocked;

            this.Users.Update(user);
            this.Users.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.OperationSuccess));
        }
    }
}
