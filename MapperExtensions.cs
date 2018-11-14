using System;

namespace mvc_album_browser
{
    public static class MapperExtensions
    {
        public static TDst MapTo<TSrc, TDst>(this TSrc src) where TSrc : class 
                                                            where TDst : class
        {
            var dst = Activator.CreateInstance<TDst>();

            var mapper = MapperConfigurator.Mapper.GetMapper<TSrc, TDst>();

            var m = (Mapper<TSrc, TDst>)mapper;
            var sourceType = m.SourceType;
            var destinationType = m.DestinationType;

            if (mapper != null)
            {
                dst = mapper.Map(src);
            }

            return dst;
        }
    }
}