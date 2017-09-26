using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public class TokenAuthenticationOptions : Microsoft.AspNetCore.Authentication.AuthenticationOptions, IOptions<TokenAuthenticationOptions>
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; } = "IDI";

        public string Audience { get; set; } = "IDI.Audience";

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(1);

        public SigningCredentials SigningCredentials { get; set; }

        public TokenAuthenticationOptions Value { get { return this; } }
    }
}
