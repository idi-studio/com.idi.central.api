using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public class TokenAuthenticationOptions : AuthenticationOptions, IOptions<TokenAuthenticationOptions>
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; } = "IDI";

        public string Audience { get; set; } = "IDI-Client";

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30);

        public SigningCredentials SigningCredentials { get; set; }

        public TokenAuthenticationOptions Value { get { return this; } }
    }
}
