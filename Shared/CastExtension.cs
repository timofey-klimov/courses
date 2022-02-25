using System;

namespace Shared
{
    public static class CastExtension
    {
        public static T To<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T ToEnum<T>(this object obj)
        {
            return (T)Enum.Parse(typeof(T), obj.ToString());
        }
    }
}
