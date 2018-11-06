using System;
using System.Collections.Generic;
using System.Linq;

using mvc_album_browser.Abstract;

namespace mvc_album_browser
{
    public class HttpJsonRepository<T> : IRepository<T> where T : class
    {
        private readonly IJsonSerializer<T> _dataset;
        public HttpJsonRepository(IJsonSerializer<T> dataset)
        {
            _dataset = dataset;
        }
        private IEnumerable<T> dataSet
        {
            get
            {
                return GetAll();
            }
        }
        public IEnumerable<T> GetAll()
        {
            return _dataset.GetAll();
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return dataSet.Where(predicate);
        }
        public T First()
        {
            return dataSet.First();
        }

        public T First(Func<T, bool> predicate)
        {
            return dataSet.First(predicate);
        }

        public T FirstOrDefault()
        {
            return dataSet.FirstOrDefault();
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return dataSet.FirstOrDefault(predicate);
        }
    }
}