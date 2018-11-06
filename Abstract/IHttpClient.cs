using System.IO;
using System.Threading.Tasks;

namespace mvc_album_browser.Abstract
{
    public interface IHttpClient<T> where T : class
    {
        Task<Stream> GetStreamAsync();
        Task<string> GetStringAsync();
    }
}