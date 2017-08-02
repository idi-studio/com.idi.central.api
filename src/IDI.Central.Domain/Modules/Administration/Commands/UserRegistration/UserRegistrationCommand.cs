using IDI.Central.Domain.Localization;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserRegistrationCommand : Command
    {
        [RequiredField(Resources.Key.DisplayName.Username)]
        [StringLength(Resources.Key.DisplayName.Username, MinLength = 6, MaxLength = 20)]
        public string UserName { get; private set; }

        [RequiredField(Resources.Key.DisplayName.Password)]
        [StringLength(Resources.Key.DisplayName.Password, MinLength = 6, MaxLength = 20)]
        public string Password { get; private set; }

        [RequiredField(Resources.Key.DisplayName.ConfirmPassword)]
        [StringLength(Resources.Key.DisplayName.ConfirmPassword, MinLength = 6, MaxLength = 20)]
        public string Confirm { get; private set; }

        public UserRegistrationCommand(string username, string password, string confirm)
        {
            this.UserName = username;
            this.Password = password;
            this.Confirm = confirm;
        }
    }
}
