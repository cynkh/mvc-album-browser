namespace mvc_album_browser
{
    public class MapperConfiguratorProviderStartupTask : IStartupTask
    {
        private readonly IMapperConfigurator _mapperConfigurator;
        public MapperConfiguratorProviderStartupTask(IMapperConfigurator mapperConfigurator)
        {
            _mapperConfigurator = mapperConfigurator;
        }
        public void OnStartup()
        {
            MapperConfiguratorProvider.GetMapperConfigurator = () => _mapperConfigurator;
        }
    }
}