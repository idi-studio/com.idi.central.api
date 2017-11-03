namespace IDI.Core.Common.Extensions
{
    public static class ReflectionExtension
    {
        public static bool TypeOf<T>(this object obj)
        {
            return obj.GetType() == typeof(T);
        }
    }
}
