using System;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class OAuthUserCreationCommand : Command
    {
        [RequiredField(Group = ValidationGroup.Create)]
        public string Name { get; set; }

        [RequiredField(Group = ValidationGroup.Create)]
        public string Login { get; set; }

        [RequiredField(Group = ValidationGroup.Create)]
        public string Email { get; set; }

        public OAuthType Type { get; set; }
    }

    public class OAuthUserCreationCommandHandler : ICommandHandler<OAuthUserCreationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(OAuthUserCreationCommand command)
        {
            string prefix = string.Empty;

            switch (command.Type)
            {
                case OAuthType.GitHub:
                    prefix = "gh";
                    break;
                case OAuthType.Wechat:
                    prefix = "wx";
                    break;
                case OAuthType.Alipay:
                    prefix = "al";
                    break;
                default:
                    break;
            }

            string username = $"{prefix}-{command.Login}";

            var user = Users.Find(e => e.UserName == username);
            var pin = new Random().Next(0, 999999).ToString("D6");
          
            if (user != null)
            {
                user.Password = Cryptography.Encrypt(pin, user.Salt);
                Users.Update(user);
                Users.Commit();
                return Result.Success(message: Localization.Get(Resources.Key.Command.AuthSuccess)).Attach("username", username).Attach("pin", pin);
            }
            else
            {
                var salt = Cryptography.Salt();
                user = new User
                {
                    UserName = username,
                    Salt = salt,
                    Password = Cryptography.Encrypt(pin, salt),
                    Profile = new UserProfile { Name = command.Name, Email = command.Email },
                };

                user.Authorize(new Role { Name = Configuration.Roles.Customers });
                Users.Add(user);
                Users.Commit();

                return Result.Success(message: Localization.Get(Resources.Key.Command.AuthSuccess)).Attach("username", username).Attach("pin", pin);
            }
        }
    }
}
