using System.Collections.Generic;
using System.Security.Claims;
using IDI.Core.Authentication;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Central.Domain.Modules.SCM.Conditions;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IDI.Central.Middlewares
{
    public class TokenAuthMiddleware : TokenAuthenticationMiddleware
    {
        public TokenAuthMiddleware(RequestDelegate next, IOptions<TokenAuthenticationOptions> options) : base(next, options) { }

        protected override List<Claim> GetIdentity(string username)
        {
            var condition = new UserIdentityQueryCondition { UserName = username };

            var result = ServiceLocator.QueryProcessor.Execute<UserIdentityQueryCondition, UserIdentity>(condition);

            if (result.Status == ResultStatus.Success)
            {
                var identity = result.Data;

                return new List<Claim>
                {
                    new Claim(ClaimTypes.Role, identity.Role, ClaimValueTypes.String),
                    new Claim(ClaimTypes.NameIdentifier, identity.NameIdentifier, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Name, identity.Name, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Gender, identity.Gender.ToString(), ClaimValueTypes.String),
                };
            }

            return null;
        }

        protected override Result Verify(string username, string password)
        {
            var command = new IdentityVerifyCommand(username, password);

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
