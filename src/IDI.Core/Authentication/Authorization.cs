using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IDI.Core.Authentication
{
    public interface IRole
    {
        string Name { get; }

        string Permissions { get; }
    }

    public interface IUser
    {
        string Name { get; }

        string Roles { get; }
    }

    public interface IAuthorization
    {
        List<IPermission> Permissions { get; }

        Dictionary<string, List<IPermission>> RolePermissions { get; }

        bool HasPermission(string[] roles, IPermission permission);
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

                Permissions.AddRange(permissions.Select(p => new Permission(module.Name, p.Name, p.Type,p.Everyone)));
            }

            RolePermissions = GroupByRole(this.Permissions);
        }

        public bool HasPermission(string[] roles, IPermission permission)
        {
            foreach (var role in roles)
            {
                if (!RolePermissions.ContainsKey(role))
                    continue;

                if (RolePermissions[role].Any(p => p.Code == permission.Code))
                    return true;
            }

            return false;
        }

        protected abstract Dictionary<string, List<IPermission>> GroupByRole(List<IPermission> permissions);
    }
}
