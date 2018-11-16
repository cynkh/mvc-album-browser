using System;
using System.Collections.Generic;
using System.Linq;

namespace mvc_album_browser
{
    public static class LinqExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var obj in collection)
            {
                action.Invoke(obj);
            }
        }
    }
}