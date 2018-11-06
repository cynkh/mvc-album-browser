using Newtonsoft.Json;

using System;
using System.IO;
using System.Collections.Generic;

using mvc_album_browser.Abstract;

namespace mvc_album_browser
{
    public class NewtonsoftHttpJsonSerializer<T> : IJsonSerializer<T>
        where T : class
    {
        // TODO : tie type together with endpoint URL
        private readonly IHttpClient<T> _httpClient;
        public NewtonsoftHttpJsonSerializer(IHttpClient<T> httpClient)
        {
            _httpClient = httpClient;
        }
        private JsonSerializer _jsonSerializer
        {
            get
            {
                return new JsonSerializer();
            }
        }
        private JsonTextReader _jsonTextReader
        {
            get
            {
                return new JsonTextReader(_textReader);
            }
        }
        private TextReader _textReader
        {
            get
            {
                return new StreamReader(_httpClient.GetStreamAsync().Result);
            }
        }
        // assumes the entire stream represents a single, 
        // deserializable object of type T
        public T Get()
        {
            var obj = Activator.CreateInstance<T>();

            try
            {
                obj = _jsonSerializer.Deserialize<T>(_jsonTextReader);
            }
            catch
            {
                // TODO log or throw if stream cannot be deserialized to type T
            }

            return obj;
        }
        // assumes the entire stream represents a collection of  
        // deserializable objects of type T
        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> coll;

            try
            {
                coll = _jsonSerializer.Deserialize<IEnumerable<T>>(_jsonTextReader);
            }
            catch
            {
                // TODO log or throw if stream cannot be deserialized to a collection of T
                coll = new List<T>();
            }

            return coll;
        }
    }
}