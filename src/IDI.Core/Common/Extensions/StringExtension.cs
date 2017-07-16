using System;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace IDI.Core.Common
{
    public static class StringExtension
    {
        public static Guid ToGuid(this string g)
            => new Guid(g);

        public static bool IsNull(this string value)
            => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);

        public static long ToUnixEpochDate(this DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public static string ToFormat(this string format, params object[] args) => string.Format(format, args);

        public static string Description(this Enum value, bool nameInstend = true)
        {
            Type type = value.GetType();

            string name = Enum.GetName(type, value);

            if (name == null)
                return null;

            FieldInfo field = type.GetField(name);

            var attribute = field.GetCustomAttributes().FirstOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstend == true)
                return name;

            return attribute.Description;
        }
    }
}
