using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common.Enums;

namespace IDI.Central.Domain.Modules.Administration
{
    public static class Extension
    {
        public static bool IsAuthorized(this Menu menu, List<Permission> permissions)
        {
            if (permissions == null || (permissions != null && permissions.Count == 0))
                return false;

            if (permissions.Any(p => p.Type == PermissionType.Query && p.ModuleId == menu.ModuleId && p.Code == menu.Code))
                return true;

            return false;
        }
    }
}
