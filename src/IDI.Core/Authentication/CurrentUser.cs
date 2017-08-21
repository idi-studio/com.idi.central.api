using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace IDI.Core.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        string Name { get; }
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor accessor;
        private readonly ClaimsPrincipal principal;
        private ISession session => accessor.HttpContext.Session;

        public bool IsAuthenticated
        {
            get
            {
                if (principal == null)
                    return false;

                return principal.Identity.IsAuthenticated;
            }
        }

        public string Name
        {
            get
            {
                if (this.IsAuthenticated)
                {
                    return principal.Identity.Name;
                }

                return string.Empty;
            }
        }

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            accessor = httpContextAccessor;

            byte[] value;

            ClaimsPrincipal principal = null;

            if (session != null && session.TryGetValue("current-user", out value))
            {
                using (var stream = new MemoryStream(value))
                using (var reader = new BinaryReader(stream))
                {
                    principal = new ClaimsPrincipal(reader);
                }
            }
        }
    }
}
