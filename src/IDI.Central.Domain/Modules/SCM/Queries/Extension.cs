using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.SCM.AggregateRoots;

namespace IDI.Central.Domain.Modules.SCM.Queries
{
    public static class Extension
    {
        public static bool IsAuthorized(this Menu menu, List<Privilege> privileges)
        {
            if (privileges == null || (privileges != null && privileges.Count == 0))
                return false;

            if (privileges.Any(p => p.PrivilegeType == PrivilegeType.View && p.ModuleId == menu.ModuleId && p.Code == menu.Code))
                return true;

            return false;
        }
    }
}
