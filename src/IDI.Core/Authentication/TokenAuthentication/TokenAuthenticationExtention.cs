using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;

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
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return context.Response.WriteAsync("Bad Request.");
        }

        public static Task InternalServerError(this HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return context.Response.WriteAsync(Result.Error(exception).ToJson());
        }

        public static Task Unauthorized(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return context.Response.WriteAsync(Result.Fail("Unauthorized.").ToJson());
        }

        public static Task OK(this HttpContext context, TokenModel token)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return context.Response.WriteAsync(Result.Success(token).ToJson());
        }
    }
}
