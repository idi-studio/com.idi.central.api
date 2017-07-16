using IDI.Core.Common;

namespace IDI.Central.Models.Administration
{
    public class SignInInput : IInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
