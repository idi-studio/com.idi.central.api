using IDI.Core.Authentication;

namespace IDI.Central.Domain
{
    public class ApplicationAuthorization : Authorization
    {
        public ApplicationAuthorization() : base("IDI.Central") { }
    }
}
