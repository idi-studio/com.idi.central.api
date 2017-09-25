using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IDI.Central.Domain.Localization;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure;
using IDI.Core.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace IDI.Central.Core
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
                context.HttpContext.User = hanlder.ValidateToken(token, validationParameters, out validatedToken);

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
        private const string jsonContent = "application/json";
        private const string code = "401";
        private static ILocalization localization;

        static ApplicationAuthorizeExtention()
        {
            localization = Runtime.GetService<ILocalization>();
        }

        public static Task Unauthorized(this HttpContext context)
        {
            context.Response.ContentType = jsonContent;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail(localization.Get(Resources.Key.Command.Unauthorized), code: code).ToJson());
        }

        public static Task TokenExpired(this HttpContext context)
        {
            context.Response.ContentType = jsonContent;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail(localization.Get(Resources.Key.Command.TokenExpired), code: code).ToJson());
        }

        public static Task TokenInvalid(this HttpContext context)
        {
            context.Response.ContentType = jsonContent;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail(localization.Get(Resources.Key.Command.TokenInvalid), code: code).ToJson());
        }

        public static Task InternalServerError(this HttpContext context, Exception exception)
        {
            context.Response.ContentType = jsonContent;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(Result.Error(exception).ToJson());
        }
    }
}
