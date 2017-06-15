using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IDI.Core.Infrastructure.Commands;

namespace IDI.Core.Infrastructure.Utils
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            var handlers = GetHandlerTypes<T>().ToList();

            var commandHandler = handlers.Select(type => (ICommandHandler<T>)type.CreateInstance()).FirstOrDefault();

            return commandHandler;
        }

        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            var handlers = typeof(T).GetTypeInfo().Assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(a => a.GetTypeInfo().IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .Where(t => t.GetInterfaces().Any(a => a.GetGenericArguments().Any(aa => aa == typeof(T)))).ToList();

            return handlers;
        }
    }
}
