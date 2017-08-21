using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Core.Authentication.TokenAuthentication
{
    internal class JwtDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string algorithm;
        private readonly TokenValidationParameters validationParameters;

        public JwtDataFormat(string algorithm, TokenValidationParameters validationParameters)
        {
            this.algorithm = algorithm;
            this.validationParameters = validationParameters;
        }

        public AuthenticationTicket Unprotect(string protectedText) => Unprotect(protectedText, null);

        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            ClaimsPrincipal principal = null;
            SecurityToken validToken = null;

            try
            {
                var handler = new JwtSecurityTokenHandler();

                principal = handler.ValidateToken(protectedText, this.validationParameters, out validToken);

                var validJwt = validToken as JwtSecurityToken;

                if (validJwt == null)
                    throw new ArgumentException("Invalid JWT");

                if (!validJwt.Header.Alg.Equals(algorithm, StringComparison.Ordinal))
                    throw new ArgumentException($"Algorithm must be '{algorithm}'");

                // Additional custom validation of JWT claims here (if any)
            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            // Validation passed. Return a valid AuthenticationTicket:
            return new AuthenticationTicket(principal, new AuthenticationProperties(), CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public string Protect(AuthenticationTicket data) => Protect(data, null);

        public string Protect(AuthenticationTicket data, string purpose)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var claim = data.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);

            if (claim == null)
                return string.Empty;

            return claim.Value;
        }
    }
}
