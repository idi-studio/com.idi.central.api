using System;

namespace IDI.Core.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonDataAttribute: Attribute
    {
        public Type Type { get; private set; }

        public JsonDataAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
