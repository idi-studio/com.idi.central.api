using System.Collections.Generic;
using System.Security.Claims;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Core.Authentication;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IDI.Central.Providers
{
    public class ApplicationAuthenticationProvider : TokenAuthenticationProvider
    {
        public ApplicationAuthenticationProvider(RequestDelegate next, IOptions<TokenAuthenticationOptions> options) : base(next, options) { }

        protected override List<Claim> GenerateClientIdentity(string clientId, string clientSecret)
        {
            return new List<Claim> {
                new Claim(ClaimTypes.AuthenticationMethod, Constants.AuthenticationMethod.ClientCredentials, ClaimValueTypes.String),
                new Claim(ClaimTypes.NameIdentifier, clientId, ClaimValueTypes.String)
            };
        }

        protected override List<Claim> GenerateUserIdentity(string username, string password)
        {
            var condition = new UserIdentityQueryCondition { UserName = username };

            var result = ServiceLocator.QueryProcessor.Execute<UserIdentityQueryCondition, UserIdentity>(condition);

            if (result.Status == ResultStatus.Success)
            {
                var identity = result.Data;

                return new List<Claim>
                {
                    new Claim(ClaimTypes.AuthenticationMethod, Constants.AuthenticationMethod.Password, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Role, identity.Role, ClaimValueTypes.String),
                    new Claim(ClaimTypes.NameIdentifier, identity.NameIdentifier, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Name, identity.Name, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Gender, identity.Gender.ToString(), ClaimValueTypes.String),
                };
            }

            return null;
        }

        protected override Result GrantUserCredential(string username, string password)
        {
            return ServiceLocator.CommandBus.Send(new UserAuthenticationCommand(username, password));
        }

        protected override Result GrantClientCredentials(string clientId, string clientSecret)
        {
            return ServiceLocator.CommandBus.Send(new ClientAuthenticationCommand(clientId, clientSecret));
        }
    }
}
