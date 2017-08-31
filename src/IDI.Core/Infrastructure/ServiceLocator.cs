using System;
using IDI.Core.Authentication;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Repositories;
using IDI.Core.Repositories.EFCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IDI.Core.Infrastructure
{
    public sealed class ServiceLocator
    {
        private static bool isInitialized;
        private static readonly object aync = new object();
        private static IServiceCollection services;
        private static ICommandBus commandBus;
        private static IQueryProcessor queryProcessor;
        private static Action AfterInitialization;

        public static ICommandBus CommandBus
        {
            get { return commandBus; }
        }

        public static IQueryProcessor QueryProcessor
        {
            get { return queryProcessor; }
        }

        public static IServiceProvider ServiceProvider
        {
            get { return services.BuildServiceProvider(); }
        }

        public static IServiceCollection Services
        {
            get { return services; }
        }

        static ServiceLocator()
        {
            AfterInitialization = () =>
            {
                commandBus = GetService<ICommandBus>();
                queryProcessor = GetService<IQueryProcessor>();
            };

            Initialize();
        }

        public static void Initialize()
        {
            if (!isInitialized)
            {
                lock (aync)
                {
                    services = new ServiceCollection();
                    services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
                    services.AddSingleton<ICommandBus, CommandBus>();
                    services.AddSingleton<IQueryBuilder, QueryBuilder>();
                    services.AddSingleton<IQueryProcessor, QueryProcessor>();
                    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
                    services.AddTransient<ICurrentUser, CurrentUser>();

                    isInitialized = true;

                    AfterInitialization();
                }
            }
        }

        public static void Clear()
        {
            if (isInitialized)
            {
                lock (aync)
                {
                    services.Clear();
                    isInitialized = false;
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
