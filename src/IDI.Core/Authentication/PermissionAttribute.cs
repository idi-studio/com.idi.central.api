using System;
using IDI.Core.Common.Enums;

namespace IDI.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionAttribute : Attribute
    {
        public string Name { get; private set; }

        public PermissionType Type { get; private set; }

        public PermissionAttribute(string name, PermissionType type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}
