using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IDI.Core.Common
{
    public static class CollectionExtension
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || (list != null && list.Count == 0);

        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || (array != null && array.Length == 0);

        public static bool Addition(this List<Claim> claims, Claim claim)
        {
            if (!claims.Any(c => c.Type == claim.Type))
            {
                claims.Add(claim);
                return true;
            }

            return false;
        }

        public static string JoinToString(this IEnumerable<string> values, string separator) => string.Join(separator, values);

        public static bool Contains(this Dictionary<string, List<string>> dictionary, string key, string item)
        {
            if (!dictionary.ContainsKey(key))
                return false;

            return dictionary[key].Contains(item);
        }
    }
}
