using System.Threading.Tasks;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IDI.Central.Providers
{
    public class ApplicationAuthorize : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string authorization = context.HttpContext.Request.Headers["Authorization"];

            if (authorization.IsNull())
                return context.HttpContext.Unauthorized();

            AuthenticateInfo authenticate = context.HttpContext.Authentication.GetAuthenticateInfoAsync("").Result;

            return base.OnActionExecutionAsync(context, next);
        }
    }

    internal static class ApplicationAuthorizeExtention
    {
        public static Task Unauthorized(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(Result.Fail("Unauthorized.", code: "401").ToJson());
        }
    }
}
