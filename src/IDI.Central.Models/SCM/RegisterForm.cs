using IDI.Core.Common;

namespace IDI.Central.Models.SCM
{
    public class RegisterForm : IForm
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }
    }
}
