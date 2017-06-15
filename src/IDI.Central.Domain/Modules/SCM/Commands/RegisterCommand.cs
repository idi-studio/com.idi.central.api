using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.SCM.Commands
{
    public class RegisterCommand : Command
    {
        [RequiredField("用户名")]
        [StringLength("用户名", MinLength = 6, MaxLength = 20)]
        public string UserName { get; private set; }

        [RequiredField("密码")]
        [StringLength("密码", MinLength = 6, MaxLength = 20)]
        public string Password { get; private set; }

        [RequiredField("确认密码")]
        [StringLength("确认密码", MinLength = 6, MaxLength = 20)]
        public string Confirm { get; private set; }

        public RegisterCommand(string username, string password, string confirm)
        {
            this.UserName = username;
            this.Password = password;
            this.Confirm = confirm;
        }
    }
}
