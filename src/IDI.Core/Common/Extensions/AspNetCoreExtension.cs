using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IDI.Core.Authentication;
using IDI.Core.Authentication.TokenAuthentication;
using IDI.Core.Infrastructure;
using IDI.Core.Localization;
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
            Runtime.AddDbContext<TContext>(options => options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()));
        }

        public static void UseTokenAuthentication<TTokenAuthenticationMiddleware>(this IApplicationBuilder app, IOptions<TokenAuthenticationOptions> options = null)
            where TTokenAuthenticationMiddleware : TokenAuthenticationProvider
        {
            app.UseMiddleware<TTokenAuthenticationMiddleware>(options ?? AuthenticationOptions.TokenOptions());
        }

        public static void UseLanguagePackage<TPackage>(this IApplicationBuilder app) where TPackage : Package
        {
            var package = Activator.CreateInstance<TPackage>();

            LanguageManager.Instance.Load(package);
        }

        public static void UseAuthorization<TAuthorization>(this IApplicationBuilder app) where TAuthorization : IAuthorization
        {
            Runtime.Services.AddSingleton(typeof(IAuthorization), typeof(TAuthorization));
        }

        public static string Get(this IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(e => e.Type == type)?.Value ?? string.Empty;
        }

        public static string AsJson(this HttpRequest request)
        {
            if (request == null)
                return string.Empty;

            var dic = new Dictionary<string, string>();
            dic.Add("scheme", request.Scheme);
            dic.Add("method", request.Method);
            dic.Add("path", request.Path);
            dic.Add("form", request.GetFormAsJson());
            dic.Add("files", request.GetFileAsJson());

            return dic.ToJson();
        }

        private static string GetFormAsJson(this HttpRequest request)
        {
            try
            {
                var form = request.Form;

                return form.Keys.Select(key => new KeyValuePair<string, string>(key, form[key])).ToJson();
            }
            catch (InvalidOperationException)
            {
                return string.Empty;
            }
        }

        private static string GetFileAsJson(this HttpRequest request)
        {
            try
            {
                var files = request.Form.Files;

                return files.Select(file => new { name = file.Name, type = file.ContentType, size = string.Format("{0:N2} KB", file.Length / 1024) }).ToJson();
            }
            catch (InvalidOperationException)
            {
                return string.Empty;
            }
        }
    }
}
