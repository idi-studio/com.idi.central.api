using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Core.Common.Enums;

namespace IDI.Core.Authentication
{
    public interface IPermission
    {
        string Name { get; }

        string Code { get; }

        PermissionType Type { get; }

        string Module { get; }

        //List<string> Roles { get; }
    }

    public class Permission : IPermission
    {
        public string Name { get; private set; }

        public PermissionType Type { get; private set; }

        public string Module { get; private set; }

        public string Code => $"{Name}-{Type.ToString()}".ToLower();

        //public List<string> Roles { get; private set; } = new List<string>();

        public Permission(string module, string name, PermissionType type)
        {
            Name = name;
            Module = module;
            Type = type;
        }

        //public void Authorize(string role)
        //{
        //    if (!Roles.Any(name => name.Equals(role, StringComparison.CurrentCultureIgnoreCase)))
        //        Roles.Add(role);
        //}
    }
}
