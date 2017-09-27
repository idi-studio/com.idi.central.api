using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Domain.Modules.Administration
{
    public static class Extension
    {
        public static void Authorize(this Role role, params Permission[] permissions)
        {
            if (permissions != null && permissions.Length > 0)
            {
                role.Permissions = permissions.GroupBy(e => e.Module).ToDictionary(e => e.Key, e => e.ToList().Select(p => p.Code).ToList()).ToJson();
            }
            else
            {
                role.Permissions = new List<string>().ToJson();
            }
        }

        public static void Authorize(this User user, params Role[] roles)
        {
            if (user.Role == null)
                throw new ArgumentException("user.Role");

            if (roles != null && roles.Length > 0)
            {
                user.Role.Roles = roles.Select(e => e.Name).ToList().ToJson();
            }
            else
            {
                user.Role.Roles = new List<string>().ToJson();
            }
        }
    }
}
