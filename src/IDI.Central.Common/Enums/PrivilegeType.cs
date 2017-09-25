using System;

namespace IDI.Central.Common.Enums
{
    [Flags]
    public enum PermissionType : int
    {
        View = 2,
        Operate = 4
    }
}
