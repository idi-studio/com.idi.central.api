using IDI.Core.Common.Enums;

namespace IDI.Core.Authentication
{
    public interface IPermission
    {
        string Name { get; }

        string Code { get; }

        PermissionType Type { get; }
    }

    public class Permission : IPermission
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public PermissionType Type { get; set; }

        public string Module { get; set; }
    }
}
