using System;

namespace IDI.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : Attribute
    {
        public string Name { get; private set; }

        public ModuleAttribute(string name)
        {
            Name = name;
        }
    }
}
