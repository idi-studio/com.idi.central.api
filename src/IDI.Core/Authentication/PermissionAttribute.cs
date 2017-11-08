using System;
using IDI.Core.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IDI.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionAttribute : Attribute, IFilterMetadata
    {
        public string Name { get; private set; }

        public PermissionType Type { get; private set; }

        public bool Everyone { get; private set; }

        public PermissionAttribute(string name, PermissionType type, bool everyone = false)
        {
            this.Name = name;
            this.Type = type;
            this.Everyone = everyone;
        }
    }
}
