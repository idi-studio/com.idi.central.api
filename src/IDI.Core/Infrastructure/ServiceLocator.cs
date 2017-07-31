using System;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Infrastructure.Utils;
using IDI.Core.Repositories;
using IDI.Core.Repositories.EFCore;
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

        //public static void AddScoped<TService, TImplementation>()
        //     where TService : class
        //     where TImplementation : class, TService
        //{
        //    services.AddScoped<TService, TImplementation>();
        //}

        //public static void AddScoped(Type serviceType, Type implementationType)
        //{
        //    services.AddScoped(serviceType, implementationType);
        //}

        //public static void AddSingleton<TService, TImplementation>()
        //    where TService : class
        //    where TImplementation : class, TService
        //{
        //    services.AddSingleton<TService, TImplementation>();
        //}

        //public static void AddTransient<TService, TImplementation>()
        //    where TService : class
        //    where TImplementation : class, TService
        //{
        //    services.AddTransient<TService, TImplementation>();
        //}

        public static void AddDbContext<TContext>(Action<DbContextOptionsBuilder> optionsAction = null) where TContext : DbContext
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<TContext>(optionsAction: optionsAction);
            services.AddScoped<DbContext, TContext>();
            services.AddScoped<IRepositoryContext, EFCoreRepositoryContext>();
            services.AddScoped<IEFCoreRepositoryContext, EFCoreRepositoryContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(EFCoreRepository<>));
        }
    }
}
