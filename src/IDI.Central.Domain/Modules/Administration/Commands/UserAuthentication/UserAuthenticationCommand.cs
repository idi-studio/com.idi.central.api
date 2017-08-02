using IDI.Central.Domain.Localization;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserAuthenticationCommand : Command
    {
        [RequiredField(Resources.Key.DisplayName.Username)]
        public string UserName { get; private set; }

        [RequiredField(Resources.Key.DisplayName.Password)]
        public string Password { get; private set; }

        public UserAuthenticationCommand(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }
    }
}
