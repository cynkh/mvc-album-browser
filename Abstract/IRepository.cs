using System;
using System.Collections.Generic;

namespace mvc_album_browser.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Func<T, bool> predicate);
        T First();
        T First(Func<T, bool> predicate);
        T FirstOrDefault();
        T FirstOrDefault(Func<T, bool> predicate);
    }
}