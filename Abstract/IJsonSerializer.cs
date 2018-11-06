using System.Collections.Generic;

namespace mvc_album_browser.Abstract
{
    public interface IJsonSerializer<T> where T : class
    {
        T Get();
        IEnumerable<T> GetAll();
    }
}