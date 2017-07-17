using System;
using System.IO;
using System.Text;
using IDI.Core.Authentication.TokenAuthentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Core.Authentication
{
    public class ApplicationAuthenticationOptions
    {
        // The secret key every token will be signed with.
        // In production, you should store this securely in environment variables
        // or a key management tool. Don't hardcode this into your application!
        private static readonly string secretkey = "mysupersecret_secretkey!123";

        private static SecurityKey Signing() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretkey));

        public static CookieAuthenticationOptions CookieOptions()
        {
            var validationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Signing(),
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            return new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                CookieName = "access_token",
                ExpireTimeSpan = TimeSpan.FromMinutes(20),
                LoginPath = new PathString("/platform/login"),
                DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(@"\\10.248.36.91\shared-auth-ticket-keys\")),
                TicketDataFormat = new JwtDataFormat(SecurityAlgorithms.HmacSha256, validationParameters)
            };
        }

        public static TokenAuthenticationOptions TokenOptions()
        {
            return new TokenAuthenticationOptions
            {
                SigningCredentials = new SigningCredentials(Signing(), SecurityAlgorithms.HmacSha256),
            };
        }
    }
}
