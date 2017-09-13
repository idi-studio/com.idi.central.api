using System;

namespace IDI.Central.Common.Enums
{
    [Flags]
    public enum PrivilegeType : int
    {
        View = 2,
        Operate = 4
    }
}
