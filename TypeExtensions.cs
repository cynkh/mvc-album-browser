using System;
using System.Linq;
using System.Reflection;

namespace mvc_album_browser
{
    public static class TypeExtensions
    {
        public static bool HasInterface<T>(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(T));
        }
    }
}