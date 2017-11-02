using System;

namespace IDI.Core.Common.Enums
{
    [Flags]
    public enum ValidationGroup
    {
        Default=1,
        Create=2,
        Update=4,
        Delete=8,
        Query=16,
        Upload = 32
    }
}
