using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IDI.Core.Authentication
{
    public interface IAuthorization
    {
        List<IPermission> Permissions { get; }
    }

    public abstract class Authorization : IAuthorization
    {
        public List<IPermission> Permissions { get; private set; } = new List<IPermission>();

        public Authorization(string assemblyName)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyName));

            var types = assembly.GetTypes().Where(t => typeof(IAuthorizable).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var type in types)
            {
                var module = type.GetCustomAttribute<ModuleAttribute>();

                var permissions = type.GetMethods().Where(m => m.GetCustomAttribute<PermissionAttribute>() != null).Select(m => m.GetCustomAttribute<PermissionAttribute>());

                Permissions.AddRange(permissions.Select(p => new Permission
                {
                    Module = module.Name,
                    Name = p.Name,
                    Code = $"{p.Name}-{p.Type.ToString()}".ToLower(),
                    Type = p.Type
                }));
            }
        }
    }
}
