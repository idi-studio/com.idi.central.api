using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Authentication;
using IDI.Core.Common.Extensions;

namespace IDI.Central.Domain.Modules.Administration
{
    public static class Extension
    {
        public static void Authorize(this Role role, params IPermission[] permissions)
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

        public static void Authorize(this Role role, params Module[] modules)
        {
            if (modules != null && modules.Length > 0)
            {
                role.Menus = modules.Select(e => e.SN).Union(modules.SelectMany(e => e.Menus).Select(e => e.SN)).ToJson();
            }
            else
            {
                role.Menus = new List<int>().ToJson();
            }
        }

        public static void Authorize(this User user, params IRole[] roles)
        {
            if (roles != null && roles.Length > 0)
            {
                user.Role.Roles = roles.Select(e => e.Name).ToList().ToJson();
            }
            else
            {
                user.Role.Roles = new List<string>().ToJson();
            }
        }

        public static List<MenuItem> UserMenus(this List<Module> source, List<int> menus)
        {
            if (menus == null || (menus != null && menus.Count == 0))
                return new List<MenuItem>();

            return source.Where(e => menus.Contains(e.SN)).OrderBy(e => e.SN).Select(e => new MenuItem
            {
                SN = e.SN,
                Name = e.Name,
                Icon = e.Icon,
                Route = e.Route,
                Sub = e.Menus.OrderBy(o => o.SN).Select(o => new MenuItem
                {
                    SN = o.SN,
                    Name = o.Name,
                    Route = o.Route
                }).ToList()
            }).ToList();
        }

        public static List<int> Menus(this List<Module> source)
        {
            return source.Select(e => e.SN).Union(source.SelectMany(e => e.Menus).Select(e => e.SN)).ToList();
        }

        public static List<int> UserMenus(this List<Role> source, List<string> userRoles)
        {
            if (userRoles == null || (userRoles != null && userRoles.Count == 0))
                return new List<int>();

            return source.Where(e => userRoles.Contains(e.Name)).SelectMany(e => e.Menus.To<List<int>>()).Distinct().ToList();
        }

        public static List<string> Roles(this User user)
        {
            if (user.Role == null || (user.Role != null && user.Role.Roles.IsNull()))
                return new List<string>();

            return user.Role.Roles.To<List<string>>();
        }
    }
}
