using System;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public class TokenAuthenticationOptions
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; } = "ExampleIssuer";

        public string Audience { get; set; } = "ExampleAudience";

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
