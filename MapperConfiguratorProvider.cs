using System;

namespace mvc_album_browser
{
    public class MapperConfiguratorProvider
    {
        public static Func<IMapperConfigurator> GetMapperConfigurator = () => null;
    }
}