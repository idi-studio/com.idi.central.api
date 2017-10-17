using System;
using System.IO;
using IDI.Core.Authentication;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Localization;
using IDI.Core.Logging;
using IDI.Core.Repositories;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IDI.Core.Infrastructure
{
    public sealed class Runtime
    {
        private static bool initialized;
        private static readonly object aync = new object();
        private static IServiceCollection services;
        public static IServiceCollection Services => services;
        public static IQuerier Querier => services.BuildServiceProvider().GetService<IQuerier>();
        public static ICommandBus CommandBus => services.BuildServiceProvider().GetService<ICommandBus>();

        public static ILoggerRepository LoggerRepository { get; private set; }

        static Runtime() { }

        public static void Initialize(IServiceCollection collection = null)
        {
            if (!initialized)
            {
                lock (aync)
                {
                    services = collection ?? new ServiceCollection();

                    var repository = LogManager.CreateRepository("IDI.Core.LoggerRepository");
                    XmlConfigurator.Configure(repository, new FileInfo("Configs/log4net.config"));

                    services.AddSingleton(LogManager.GetLogger(repository.Name, typeof(Runtime)));
                    services.AddSingleton<ILogger, Log4NetLogger>();
                    services.AddSingleton<ILocalization, Globalization>();
                    services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
                    services.AddScoped<ICommandBus, CommandBus>();
                    services.AddSingleton<IQueryBuilder, QueryBuilder>();
                    services.AddScoped<IQuerier, Querier>();
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
            return services.BuildServiceProvider().GetService<T>().InjectedProperties();
        }

        public static object GetService(Type serviceType)
        {
            return services.BuildServiceProvider().GetService(serviceType).InjectedProperties();
        }

        public static void AddDbContext<TContext>(Action<DbContextOptionsBuilder> optionsAction = null) where TContext : DbContext
        {
            services.AddDbContextPool<TContext>(optionsAction: optionsAction);
            services.AddScoped<DbContext, TContext>();
            services.AddScoped<IRepositoryContext, RepositoryContext>();
            services.AddScoped<ITransaction, Transaction>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IQueryableRepository<>), typeof(Repository<>));
        }

        public static void UseAuthorization<TAuthorization>() where TAuthorization : IAuthorization
        {
            services.AddSingleton(typeof(IAuthorization), typeof(TAuthorization));
        }
    }
}
