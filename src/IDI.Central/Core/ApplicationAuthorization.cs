using IDI.Core.Authentication;

namespace IDI.Central.Core
{
    public class ApplicationAuthorization : Authorization
    {
        public ApplicationAuthorization() : base("IDI.Central") { }
    }
}
