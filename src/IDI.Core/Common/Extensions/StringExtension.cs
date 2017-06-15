using System;

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
    }
}
