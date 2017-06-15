using System;
using System.Reflection;

namespace IDI.Core.Infrastructure.Verification
{
    public class ValidationContext
    {
        public object Instance { get; private set; }

        public Type ObjectType { get; private set; }

        public PropertyInfo Property { get; private set; }

        public ValidationContext(object instance, PropertyInfo property)
        {
            this.Instance = instance;
            this.ObjectType = instance.GetType();
            this.Property = property;
        }
    }
}
