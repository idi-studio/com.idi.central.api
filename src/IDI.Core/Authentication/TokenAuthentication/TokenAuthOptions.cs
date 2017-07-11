using System;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public class TokenAuthOptions
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; } = "IDI";

        public string Audience { get; set; } = "IDI-Audience";

        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(6);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
