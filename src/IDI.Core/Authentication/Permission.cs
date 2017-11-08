using IDI.Core.Common.Enums;

namespace IDI.Core.Authentication
{
    public interface IPermission
    {
        string Name { get; }

        string Code { get; }

        PermissionType Type { get; }

        string Module { get; }

        bool Everyone { get; }
    }

    public sealed class Permission : IPermission
    {
        public string Name { get; private set; }

        public PermissionType Type { get; private set; }

        public string Module { get; private set; }

        public string Code => $"{Module}-{Name}-{Type.ToString()}".ToLower();

        public bool Everyone { get; private set; }

        public Permission(string module, string name, PermissionType type, bool everyone)
        {
            Name = name;
            Module = module;
            Type = type;
            Everyone = everyone;
        }
    }
}
