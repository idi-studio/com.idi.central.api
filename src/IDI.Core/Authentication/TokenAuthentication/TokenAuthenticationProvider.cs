using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IDI.Core.Authentication.TokenAuthentication
{
    internal static class TokenAuthExtention
    {
        public static string GrantType(this HttpContext context)
        {
            return context.Request.Form["grant_type"];
        }

        public static bool TryGetClientCredential(this HttpContext context, out string clientId, out string clientSecret)
        {
            clientId = string.Empty; clientSecret = string.Empty;
            string authorization = context.Request.Headers["Authorization"];

            if (!authorization.StartsWith(Constants.AuthenticationScheme.Basic))
                return false;

            authorization = authorization.Split(new char[] { ' ' }).Last();
            authorization = Encoding.UTF8.GetString(Convert.FromBase64String(authorization));

            var parameters = authorization.Split(new char[] { ':' });

            clientId = parameters.FirstOrDefault();
            clientSecret = parameters.LastOrDefault();

            if (clientId.IsNull() || clientSecret.IsNull())
                return false;

            return true;
        }

        public static bool TryGetUserCredential(this HttpContext context, out string username, out string password)
        {
            username = context.Request.Form["username"];
            password = context.Request.Form["password"];

            if (username.IsNull() || password.IsNull())
                return false;

            return true;
        }

        public static Task BadRequest(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsync("Bad Request.");
        }

        public static Task InternalServerError(this HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(Result.Error(exception).ToJson());
        }

        public static Task Unauthorized(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail("Unauthorized.").ToJson());
        }

        public static Task OK(this HttpContext context, TokenModel token)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            return context.Response.WriteAsync(Result.Success(token).ToJson());
        }
    }

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
            if (!context.Request.Path.Equals(options.Path, StringComparison.Ordinal))
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
                return GenerateToken(context, identity, secret, generateIdentity);

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
    }
}
