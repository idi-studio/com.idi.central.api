using System.IO;
using Newtonsoft.Json;

namespace IDI.Core.Common.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this T obj, TypeNameHandling typeNameHandling = TypeNameHandling.None)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = typeNameHandling;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static dynamic ToDynamic(this string json)
        {
            return new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(json)));
        }

        public static T To<T>(this string json)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            try
            {
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
