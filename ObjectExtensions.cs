using System;

namespace mvc_album_browser
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object obj)
        {
            return ReferenceEquals(obj, null) ? default(T) : (T)obj;
        }
    }
}