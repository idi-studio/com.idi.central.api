using System;
using System.Linq;
using System.Reflection;
using IDI.Core.Infrastructure.DependencyInjection;

namespace IDI.Core.Infrastructure.Utils
{
    internal static class UtilExtensions
    {
        public static object CreateInstance(this Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsInterface || typeInfo.IsAbstract)
                return null;

            var constructor = GetConstructor(type);

            if (constructor == null)
                return null;

            object[] parameters = constructor.GetParameters().Select(p => ServiceLocator.GetService(p.ParameterType)).ToArray();

            object instance = constructor.Invoke(parameters).InjectedProperties();

            return instance;
        }

        public static T InjectedProperties<T>(this T service)
        {
            InitializeInjectedProperties(service);

            return service;
        }

        public static object InjectedProperties(this object service)
        {
            InitializeInjectedProperties(service);

            return service;
        }

        private static ConstructorInfo GetConstructor(Type type)
        {
            var constructors = type.GetConstructors();

            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null) ?? constructors.FirstOrDefault();
        }

        private static void InitializeInjectedProperties(object service)
        {
            var properties = service.GetType().GetProperties().Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null).ToList();

            properties.ForEach(p => p.SetValue(service, ServiceLocator.GetService(p.PropertyType)));
        }
    }
}
