using IDI.Core.Common.Enums;

namespace IDI.Core.Authentication
{
    public interface IPermission
    {
        string Name { get;  }

        string Code { get; }

        PermissionType Type { get; }
    }
}
