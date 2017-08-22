using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IDI.Core.Common.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        //public static void Set<T>(this ISession session, string key, T value)
        //{
        //    session.Set(key, value.GetBuffer());
        //}

        //public static T Get<T>(this ISession session, string key)
        //{
        //    var value = session.Get(key);

        //    return value.ConvertTo<T>();
        //}

        //private static byte[] GetBuffer<T>(this T value)
        //{
        //    byte[] buffer;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        IFormatter formatter = new BinaryFormatter();
        //        formatter.Serialize(ms, value);
        //        buffer = ms.GetBuffer();
        //    }
        //    return buffer;
        //}

        //public static T ConvertTo<T>(this byte[] buffer)
        //{
        //    T value = default(T);

        //    if (buffer == null)
        //        return value;

        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        IFormatter formatter = new BinaryFormatter();
        //        value = (T)formatter.Deserialize(ms);
        //    }

        //    return value;
        //}
    }
}
