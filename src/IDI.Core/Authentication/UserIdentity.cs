using System.Linq;
using System.Security.Claims;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Core.Authentication
{
    public class UserIdentity : IQueryResult
    {
        public string NameIdentifier { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Gender { get; set; }

        public bool IsAuthenticated { get; set; } = false;

        public UserIdentity() { }

        public UserIdentity(ClaimsPrincipal principal)
        {
            this.IsAuthenticated = principal.Identity.IsAuthenticated;
            this.Name = principal.Identity.Name;
            this.NameIdentifier = principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            this.Role = principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value ?? string.Empty;
            this.Gender = principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Gender)?.Value ?? string.Empty;
        }
    }
}
