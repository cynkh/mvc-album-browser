using System.IO;
using System.Threading.Tasks;

using mvc_album_browser.Abstract;

namespace mvc_album_browser
{
    public class HttpReader<T> where T : class
    {
        private readonly IHttpClient<T> _httpClient;
        public HttpReader(IHttpClient<T> httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Stream> GetStreamAsync()
        {
            return await _httpClient.GetStreamAsync();
        }
        public async Task<string> GetStringAsync()
        {
            return await _httpClient.GetStringAsync();
        }
    }
}