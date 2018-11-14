using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace mvc_album_browser
{
    public class MapperConfigurator
    {
        private IEnumerable<MapperConfiguration> _maps;
        private static MapperConfigurator _mapperConfigurator = null;
        public MapperConfigurator()
        {
            _maps = new List<MapperConfiguration>();
        }
        public static MapperConfigurator Mapper
        {
            get
            {
                if (_mapperConfigurator == null)
                {
                    _mapperConfigurator = new MapperConfigurator();
                }

                return _mapperConfigurator;
            }
        }
        public void SetMap<TSource, TResult>(Func<TSource, TResult> map) where TSource : class 
                                                                        where TResult : class
        {
            var maps = _maps.ToList();

            var existingMap = _maps.FirstOrDefault(m => !m.HasConfigurationString && 
                m.TSource.Equals(typeof(TSource)) && 
                m.TResult.Equals(typeof(TResult)));

            var config = existingMap ?? new MapperConfiguration();

            if (existingMap != null)
            {
                maps.Remove(existingMap);
            }
            else
            {
                config.TSource = typeof(TSource);
                config.TResult = typeof(TResult);
            }

            config.Map = new Mapper<TSource, TResult>(map);

            maps.Add(config);

            _maps = maps;
        }

        public IMapper<TSource, TResult> GetMapper<TSource, TResult>() where TSource : class 
                                                                        where TResult : class 
        {
            var map = _maps.FirstOrDefault(m => m != null && 
                !m.HasConfigurationString && 
                m.TSource.Equals(typeof(TSource)) && 
                m.TResult.Equals(typeof(TResult))) ?? new MapperConfiguration();

            return map.Map as IMapper<TSource, TResult> ?? 
                new Mapper<TSource, TResult>();
        }
    }
    public class MapperConfiguration
    {
        public Type TSource { get; set; }
        public Type TResult { get; set; }
        public object Map { get; set; }
        public string ConfigurationString { get; set; }
        public bool HasConfigurationString { get; set; }
    }
}