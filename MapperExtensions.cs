using System;

namespace mvc_album_browser
{
    public static class MapperExtensions
    {
        private static IMapperConfigurator _mapperConfigurator
        {
            get
            {
                return MapperConfiguratorProvider.GetMapperConfigurator();
            }
        }
        public static TDst MapTo<TSrc, TDst>(this TSrc src) where TSrc : class 
                                                            where TDst : class
        {
            var dst = Activator.CreateInstance<TDst>();

            var mapper = _mapperConfigurator.GetMapper<TSrc, TDst>();

            if (mapper != null)
            {
                dst = mapper.Map(src);
            }

            return dst;
        }
    }
}