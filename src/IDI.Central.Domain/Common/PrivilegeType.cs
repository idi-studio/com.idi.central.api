using System;

namespace IDI.Central.Domain.Common
{
    [Flags]
    public enum PrivilegeType
    {
        View = 2,
        Operate = 4
    }
}
