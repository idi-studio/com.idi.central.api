using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Identity.Commands
{
    public class IdentityVerifyCommand : Command
    {
        [RequiredField("用户名")]
        public string UserName { get; private set; }

        [RequiredField("密码")]
        public string Password { get; private set; }

        public IdentityVerifyCommand(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }
    }
}
