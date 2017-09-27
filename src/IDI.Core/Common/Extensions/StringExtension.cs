using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using IDI.Core.Common.Extensions;

namespace System
{
    public static class StringExtension
    {
        public static Guid ToGuid(this string value)
            => new Guid(value);

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

        public static string[] Split(this string value, string separator)
        {
            return value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string TrimContiguousSpaces(this string input)
        {
            return Regex.Replace(input.Trim(), "\\s+", " ");
        }

        public static List<string> AsList(this string value)
        {
            return value.To<List<string>>();
        }

        public static string AsBase64(this byte[] data, string contentType)
        {
            if (contentType.IsNull())
                return string.Empty;

            if (data == null || (data != null && data.Length == 0))
                return string.Empty;

            return $"data:{contentType};base64,{Convert.ToBase64String(data)}";
        }

        public static string AsCode(this Guid guid)
        {
            return guid.ToString("N").ToUpper();
        }
    }
}
