using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Central.Providers
{
    public class ApplicationAuthorize : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString().Split(new char[] { ' ' });

            var scheme = authorization.FirstOrDefault();
            var token = authorization.LastOrDefault();

            if (token.IsNull() || scheme.IsNull() || !scheme.Equals(Constants.AuthenticationScheme.Bearer, StringComparison.OrdinalIgnoreCase))
                return context.HttpContext.Unauthorized();

            var hanlder = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;

            var options = ApplicationAuthenticationOptions.TokenOptions();

            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = options.SigningCredentials.Key
            };

            try
            {
                var claims = hanlder.ValidateToken(token, validationParameters, out validatedToken);

                return base.OnActionExecutionAsync(context, next);
            }
            catch (SecurityTokenExpiredException)
            {
                return context.HttpContext.TokenExpired();
            }
            catch (ArgumentException)
            {
                return context.HttpContext.TokenInvalid();
            }
            catch (SecurityTokenValidationException)
            {
                return context.HttpContext.TokenInvalid();
            }
        }
    }

    internal static class ApplicationAuthorizeExtention
    {
        public static Task Unauthorized(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail("Unauthorized", code: "401").ToJson());
        }

        public static Task TokenExpired(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail("TokenExpired", code: "401").ToJson());
        }

        public static Task TokenInvalid(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail("TokenInvalid", code: "401").ToJson());
        }
    }
}
