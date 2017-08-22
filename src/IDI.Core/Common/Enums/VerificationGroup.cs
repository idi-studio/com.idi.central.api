using System;

namespace IDI.Core.Common.Enums
{
    [Flags]
    public enum VerificationGroup
    {
        Default=1,
        Create=2,
        Update=4,
        Delete=8,
        Query=16
    }
}
