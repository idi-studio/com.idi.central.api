using System;

namespace IDI.Core.Infrastructure.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }
}
