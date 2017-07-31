using IDI.Central.Domain;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Localization.Packages;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IDI.Central
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSqlServer<CentralContext>(Configuration.GetConnectionString("DefaultConnection"));

            // Inject an implementation of ISwaggerProvider with defaulted settings applied  
            services.AddSwaggerGen();

            #region 跨域
            var urls = Configuration["Cores"].Split(',');
            services.AddCors(options => options.AddPolicy(Constants.Policy.AllowCorsDomain, builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
            services.Configure<MvcOptions>(options => { options.Filters.Add(new CorsAuthorizationFilterFactory(Constants.Policy.AllowCorsDomain)); });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseCookieAuthentication(AuthOptions.CookieOptions());

            app.UseTokenAuthentication<ApplicationAuthenticationProvider>();

            // Enable middleware to serve generated Swagger as a JSON endpoint  
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)  
            app.UseSwaggerUi();

            app.UseStaticFiles();

            app.UseCors(Constants.Policy.AllowCorsDomain);

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "default", template: "{controller=Platform}/{action=Login}/{id?}");
            //});
            app.UseLanguagePackage<PackageCentral>();
            app.UseLocalization<Localization>();
        }
    }
}
