using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;

using mvc_album_browser.Abstract;

namespace mvc_album_browser
{
    // this class should be considered transient and is meant to be scoped 
    // in a using(..) block
    public class NewtonsoftJsonSerializer<T> : IJsonSerializer<T>, IDisposable 
        where T : class
    {
        private readonly Stream _stream;
        private readonly TextReader _textReader;
        private readonly JsonTextReader _jsonTextReader;
        private readonly JsonSerializer _jsonSerializer;
        public NewtonsoftJsonSerializer(Stream stream)
        {
            _stream = stream;
            _textReader = new StreamReader(_stream);
            _jsonTextReader = new JsonTextReader(_textReader);
            _jsonSerializer = new JsonSerializer();
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
        public void Dispose()
        {
            _jsonTextReader.Close();
            _textReader.Dispose();
            _stream.Dispose();
        }
    }
}