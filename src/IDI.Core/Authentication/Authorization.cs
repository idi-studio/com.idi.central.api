using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IDI.Core.Authentication
{
    public interface IAuthorization
    {
        List<IPermission> Permissions { get; }

        Dictionary<string, List<IPermission>> RolePermissions { get; }
    }

    public abstract class Authorization : IAuthorization
    {
        public List<IPermission> Permissions { get; private set; } = new List<IPermission>();

        public Dictionary<string, List<IPermission>> RolePermissions { get; private set; } = new Dictionary<string, List<IPermission>>();

        public Authorization(string assemblyName)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyName));

            var types = assembly.GetTypes().Where(t => typeof(IAuthorizable).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var type in types)
            {
                var module = type.GetCustomAttribute<ModuleAttribute>();

                var permissions = type.GetMethods().Where(m => m.GetCustomAttribute<PermissionAttribute>() != null).Select(m => m.GetCustomAttribute<PermissionAttribute>());

                Permissions.AddRange(permissions.Select(p => new Permission(module.Name, p.Name, p.Type)));
            }
        }

        public void Authorize(string role, IPermission permission)
        {
            var current = Permissions.FirstOrDefault(p => p.Code == permission.Code && p.Module == permission.Module);

            if (current == null)
                return;

            if (RolePermissions.ContainsKey(role))
            {
                RolePermissions[role].Add(current);
            }
            else
            {
                RolePermissions.Add(role, new List<IPermission> { current });
            }
        }
    }
}
