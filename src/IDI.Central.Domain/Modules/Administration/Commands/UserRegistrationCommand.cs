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
    public class UserRegistrationCommand : Command
    {
        [RequiredField]
        [StringLength(MinLength = 6, MaxLength = 20)]
        public string UserName { get; private set; }

        [RequiredField]
        [StringLength(MinLength = 6, MaxLength = 20)]
        public string Password { get; private set; }

        [RequiredField]
        [StringLength(MinLength = 6, MaxLength = 20)]
        public string Confirm { get; private set; }

        public UserRegistrationCommand(string username, string password, string confirm)
        {
            this.UserName = username;
            this.Password = password;
            this.Confirm = confirm;
        }
    }

    public class UserRegistrationCommandHandler : ICommandHandler<UserRegistrationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(UserRegistrationCommand command)
        {
            if (command.Password != command.Confirm)
                return Result.Fail(Localization.Get(Resources.Key.Command.PasswordMismatch));

            if (this.Users.Exist(u => u.UserName == command.UserName))
                return Result.Fail(Localization.Get(Resources.Key.Command.UsernameRegistered));

            var salt = Cryptography.Salt();
            var user = new User
            {
                UserName = command.UserName,
                Salt = salt,
                Password = Cryptography.Encrypt(command.Password, salt),
                Profile = new UserProfile { Name = command.UserName }
            };

            this.Users.Add(user);
            this.Users.Commit();
            //this.Users.Context.Dispose();

            return Result.Success(message: Localization.Get(Resources.Key.Command.RegisterSuccess));
        }
    }
}
