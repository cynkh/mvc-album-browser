using System;
using System.Collections.Generic;
using System.Linq;

namespace mvc_album_browser
{
    public interface IMapperConfigurator
    {
        IMapperConfigurator SetMapper<TSource, TResult>(Func<TSource, TResult> map, string configurationString = null)
            where TSource : class
            where TResult : class;
        IMapper<TSource, TResult> GetMapper<TSource, TResult>(string configurationString = null)
            where TSource : class
            where TResult : class;
    }
}