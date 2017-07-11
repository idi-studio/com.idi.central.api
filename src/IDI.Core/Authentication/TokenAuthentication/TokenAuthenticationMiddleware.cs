using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public abstract class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly TokenAuthenticationOptions options;

        public TokenAuthenticationMiddleware(RequestDelegate next, IOptions<TokenAuthenticationOptions> options)
        {
            this.next = next;
            this.options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(options.Path, StringComparison.Ordinal))
            {
                return next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync(Result.Fail("Bad request.").ToJson());
            }

            return GenerateToken(context);
        }

        protected virtual Result GrantClientCredentials(string clientId, string clientSecret)
        {
            if (clientId.IsNull() || clientSecret.IsNull())
                return Result.Fail("Invalid Authentication.");

            return Result.Success(message: "OK");
        }

        protected virtual Result GrantPassword(string username, string password)
        {
            if (username.IsNull() || password.IsNull())
                return Result.Fail("Invalid Authentication.");

            return Result.Success(message: "OK");
        }

        protected abstract List<Claim> GetIdentity(string username);

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            try
            {
                var result = Verify(username, password);

                if (result.Status == ResultStatus.Success)
                {
                    var now = DateTime.UtcNow;

                    var claims = GetIdentity(username);

                    if (claims != null)
                    {
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

                        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                        result.Details.Add("redirect_url", $"/account/signin?token={token}");
                        result.Details.Add("access_token", token);
                        result.Details.Add("expires_in", (int)options.Expiration.TotalSeconds);
                    }
                    else
                    {
                        result = Result.Fail($"用户'{username}'身份验证失败!");
                    }
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(Result.Error(ex), new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
        }
    }
}
