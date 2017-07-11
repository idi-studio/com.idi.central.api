using System.IO;
using Newtonsoft.Json;

namespace IDI.Core.Common.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this T obj, TypeNameHandling typeNameHandling = TypeNameHandling.All)
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
    }
}
