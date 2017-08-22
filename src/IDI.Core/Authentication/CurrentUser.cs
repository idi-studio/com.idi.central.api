using System.Security.Claims;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace IDI.Core.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        string NameIdentifier { get; }

        string Name { get; }

        string Role { get; }

        string Gender { get; }
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor accessor;
        private readonly ClaimsPrincipal principal;

        public bool IsAuthenticated { get => principal.Identity.IsAuthenticated; }
        public string Name => principal.Identity.Name;
        public string Role => principal.Claims.Get(ClaimTypes.Role);
        public string Gender => principal.Claims.Get(ClaimTypes.Gender);
        public string NameIdentifier => principal.Claims.Get(ClaimTypes.NameIdentifier);

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            accessor = httpContextAccessor;

            if (accessor.HttpContext != null)
            {
                this.principal = accessor.HttpContext.User;
            }
            else
            {
                this.principal = new ClaimsPrincipal();
            }
        }
    }
}
