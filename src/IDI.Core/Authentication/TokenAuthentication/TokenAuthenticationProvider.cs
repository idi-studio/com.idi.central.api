using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using IDI.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public abstract class TokenAuthenticationProvider
    {
        private readonly RequestDelegate next;
        private readonly TokenAuthenticationOptions options;

        public TokenAuthenticationProvider(RequestDelegate next, IOptions<TokenAuthenticationOptions> options)
        {
            this.next = next;
            this.options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(options.Path, StringComparison.Ordinal) || context.Request.Method.Equals(HttpMethods.Options))
                return next(context);

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals(HttpMethods.Post) || !context.Request.HasFormContentType)
                return context.BadRequest();

            string identity = string.Empty, secret = string.Empty;
            Result result = null;
            Func<string, string, List<Claim>> generateIdentity;

            switch (context.GrantType())
            {
                case Constants.AuthenticationMethod.ClientCredentials:
                    if (context.TryGetClientCredential(out identity, out secret))
                        result = GrantClientCredentials(identity, secret);
                    generateIdentity = GenerateClientIdentity;
                    break;
                case Constants.AuthenticationMethod.Password:
                    if (context.TryGetUserCredential(out identity, out secret))
                        result = GrantUserCredential(identity, secret);
                    generateIdentity = GenerateUserIdentity;
                    break;
                default:
                    return context.BadRequest();
            }

            if (result != null && result.Status == ResultStatus.Success)
            {
                Log(identity, AuthorizeResult.Accept);
                return GenerateToken(context, identity, secret, generateIdentity);
            }

            if (result != null && result.Status == ResultStatus.Fail)
            {
                Log(identity, AuthorizeResult.Reject, result.Message);
                return context.OK(result);
            }

            return context.Unauthorized();
        }

        protected virtual Result GrantClientCredentials(string clientId, string clientSecret)
        {
            if (clientId.IsNull() || clientSecret.IsNull())
                return Result.Fail("Invalid Authentication.");

            return Result.Success(message: "OK");
        }

        protected virtual Result GrantUserCredential(string username, string password)
        {
            if (username.IsNull() || password.IsNull())
                return Result.Fail("Invalid Authentication.");

            return Result.Success(message: "OK");
        }

        protected abstract List<Claim> GenerateUserIdentity(string username, string password);

        protected abstract List<Claim> GenerateClientIdentity(string clientId, string clientSecret);

        private async Task GenerateToken(HttpContext context, string identity, string secret, Func<string, string, List<Claim>> generate)
        {
            try
            {
                var now = DateTime.UtcNow;

                var claims = generate(identity, secret);

                if (claims == null || (claims != null && claims.Count == 0))
                    await context.Unauthorized();

                //random nonce
                claims.Addition(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                //issued timestamp
                claims.Addition(new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

                var jwt = new JwtSecurityToken(
                    issuer: options.Issuer,
                    audience: options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(options.Expiration),
                    signingCredentials: options.SigningCredentials);

                var token = new TokenModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                    ExpiresIn = (int)options.Expiration.TotalSeconds
                };

                await context.OK(token);
            }
            catch (Exception ex)
            {
                await context.InternalServerError(ex);
            }
        }

        private void Log(string username, AuthorizeResult result, string reason = "-")
        {
            Runtime.GetService<ILogger>().InfoFormat("|{0}|{1}|{2}|{3}|", username ?? "-", "sign-in", result.ToString().ToLower(), reason);
        }
    }
}
