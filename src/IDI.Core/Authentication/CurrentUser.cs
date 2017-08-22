using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace IDI.Core.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        UserIdentity Identity { get; }
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor accessor;
        private ISession session => accessor.HttpContext.Session;

        public bool IsAuthenticated
        {
            get
            {
                return this.Identity != null;
            }
        }

        public UserIdentity Identity { get; private set; }

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            accessor = httpContextAccessor;

            this.Identity = session.Get<UserIdentity>(Constants.SessionKey.CurrentUser);
        }
    }
}
