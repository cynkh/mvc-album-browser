using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace mvc_album_browser
{
    public class MapperConfigurator : IMapperConfigurator
    {
        private IEnumerable<MapperConfiguration> _maps;
        public MapperConfigurator()
        {
            _maps = new List<MapperConfiguration>();
        }
        public IMapperConfigurator SetMapper<TSource, TResult>(Func<TSource, TResult> map, 
            string configurationString = null)
            where TSource : class 
            where TResult : class
        {
            var maps = _maps.ToList();

            var existingConfig = _getExistingConfiguration<TSource, TResult>(configurationString);

            var config = existingConfig ?? new MapperConfiguration();

            if (existingConfig != null)
            {
                maps.Remove(existingConfig);
            }
            else
            {
                config.TSource = typeof(TSource);
                config.TResult = typeof(TResult);
                config.HasConfigurationString = configurationString != null;
                config.ConfigurationString = configurationString;
            }

            config.Map = new Mapper<TSource, TResult>(map);

            maps.Add(config);

            _maps = maps;

            return this;
        }
        public IMapper<TSource, TResult> GetMapper<TSource, TResult>(string configurationString = null)
            where TSource : class 
            where TResult : class 
        {
            var config = _getExistingConfiguration<TSource, TResult>(configurationString) ?? 
                new MapperConfiguration();

            return (config.Map ?? new Mapper<TSource, TResult>()) as IMapper<TSource, TResult>;
        }
        private MapperConfiguration _getExistingConfiguration<TSource, TResult>(string configurationString = null)
            where TSource : class
            where TResult : class
        {
            return _maps.FirstOrDefault(c => 
                c.TSource.Equals(typeof(TSource)) && 
                c.TResult.Equals(typeof(TResult)) && 
                ((configurationString == null && !c.HasConfigurationString) || 
                    (configurationString != null && 
                        c.HasConfigurationString && 
                        c.ConfigurationString == configurationString)
                ));
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