using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserRegistrationCommand : Command
    {
        [RequiredField("username")]
        [StringLength("username", MinLength = 6, MaxLength = 20)]
        public string UserName { get; private set; }

        [RequiredField("password")]
        [StringLength("password", MinLength = 6, MaxLength = 20)]
        public string Password { get; private set; }

        [RequiredField("confirm-password")]
        [StringLength("confirm-password", MinLength = 6, MaxLength = 20)]
        public string Confirm { get; private set; }

        public UserRegistrationCommand(string username, string password, string confirm)
        {
            this.UserName = username;
            this.Password = password;
            this.Confirm = confirm;
        }
    }
}
