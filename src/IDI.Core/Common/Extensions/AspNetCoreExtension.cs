using IDI.Core.Authentication;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IDI.Core.Common.Extensions
{
    public static class AspNetCoreExtension
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers["x-requested-with"] == "XMLHttpRequest";
        }

        public static void AddSqlServer<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            ServiceLocator.AddDbContext<TContext>(options => options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()));
        }

        public static void UseTokenAuthentication<TTokenAuthenticationMiddleware>(this IApplicationBuilder app, IOptions<TokenAuthenticationOptions> options = null)
            where TTokenAuthenticationMiddleware : TokenAuthenticationMiddleware
        {
            app.UseMiddleware<TTokenAuthenticationMiddleware>(options ?? AuthOptions.TokenOptions());
        }
    }
}
