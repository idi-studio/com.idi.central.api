using System;
using System.IO;
using IDI.Core.Authentication;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Localization;
using IDI.Core.Logging;
using IDI.Core.Repositories;
using IDI.Core.Repositories.EFCore;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IDI.Core.Infrastructure
{
    public sealed class ServiceLocator
    {
        private static bool initialized;
        private static readonly object aync = new object();
        private static IServiceCollection services;

        public static ILoggerRepository LoggerRepository { get; private set; }

        public static IServiceProvider ServiceProvider
        {
            get { return services.BuildServiceProvider(); }
        }

        public static IServiceCollection Services
        {
            get { return services; }
        }

        static ServiceLocator() { }

        public static void Initialize(IServiceCollection collection)
        {
            if (!initialized)
            {
                lock (aync)
                {
                    services = collection ?? new ServiceCollection();

                    var repository = LogManager.CreateRepository("IDI.Core.LoggerRepository");
                    XmlConfigurator.Configure(repository, new FileInfo("Configs/log4net.config"));

                    services.AddSingleton(LogManager.GetLogger(repository.Name, typeof(ServiceLocator)));
                    services.AddSingleton<ILogger, Log4NetLogger>();
                    services.AddSingleton<ILocalization, Globalization>();
                    services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
                    services.AddScoped<ICommandBus, CommandBus>();
                    services.AddSingleton<IQueryBuilder, QueryBuilder>();
                    services.AddScoped<IQueryProcessor, QueryProcessor>();
                    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
                    services.AddTransient<ICurrentUser, CurrentUser>();

                    initialized = true;
                }
            }
        }

        public static void Clear()
        {
            if (initialized)
            {
                lock (aync)
                {
                    services.Clear();
                    initialized = false;
                }
            }
        }

        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>().InjectedProperties();
        }

        public static object GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType).InjectedProperties();
        }

        public static void AddDbContext<TContext>(Action<DbContextOptionsBuilder> optionsAction = null) where TContext : DbContext
        {
            services.AddDbContextPool<TContext>(optionsAction: optionsAction);
            services.AddScoped<DbContext, TContext>();
            services.AddScoped<IRepositoryContext, EFCoreRepositoryContext>();
            services.AddScoped<IEFCoreRepositoryContext, EFCoreRepositoryContext>();
            services.AddTransient(typeof(IRepository<>), typeof(EFCoreRepository<>));
            services.AddTransient(typeof(IQueryableRepository<>), typeof(EFCoreRepository<>));
        }
    }
}
