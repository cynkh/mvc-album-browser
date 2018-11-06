using System.Net.Http;

using System.IO;
using System.Threading.Tasks;

using mvc_album_browser.Abstract;

namespace mvc_album_browser.Services
{
    public class HttpClientBase<T> : HttpClient, IHttpClient<T> where T : class
    {
        private readonly string _url;
        public HttpClientBase(string url) : base()
        {
            _url = url;
        }
        public Task<Stream> GetStreamAsync()
        {
            return base.GetStreamAsync(_url);
        }

        public Task<string> GetStringAsync()
        {
            return base.GetStringAsync(_url);
        }
    }
}