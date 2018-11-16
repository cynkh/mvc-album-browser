using System;

namespace mvc_album_browser
{
    public class MapperConfiguratorProvider
    {
        public static Func<IMapperConfigurator> GetMapperConfigurator = () => null;
        public static IMapperConfigurator Configurator
        {
            get
            {
                return GetMapperConfigurator();
            }
        }
    }
}