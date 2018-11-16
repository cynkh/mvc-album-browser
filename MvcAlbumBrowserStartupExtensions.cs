using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Configuration;

using mvc_album_browser;
using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcAlbumBrowserStartupExtensions
    {
        public static IServiceCollection AddMvcAlbumBrowser(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddSingleton<IMapperConfigurator, MapperConfigurator>()
                .AddSingleton<MapperConfiguratorProviderStartupTask, MapperConfiguratorProviderStartupTask>()
                .AddSingleton<MapperConfigurationStartupTask, MapperConfigurationStartupTask>()
                .AddTransient(typeof(IJsonSerializer<>), typeof(NewtonsoftHttpJsonSerializer<>))
                .addHttpEntityClients(configuration)
                .addHttpJsonRepositories()
                .addModelServices()
                .runStartupTasks();
        }
        private static IServiceCollection addHttpEntityClients(this IServiceCollection services, IConfigurationRoot configuration)
        {
            const string USERS_URL = "UsersUrl";
            const string ALBUMS_URL = "AlbumsUrl";
            const string PHOTOS_URL = "PhotosUrl";
            const string POSTS_URL = "PostsUrl";

            var usersUrl = configuration.GetConnectionString(USERS_URL);
            var albumsUrl = configuration.GetConnectionString(ALBUMS_URL);
            var photosUrl = configuration.GetConnectionString(PHOTOS_URL);
            var postsUrl = configuration.GetConnectionString(POSTS_URL);

            return services
                .AddTransient(typeof(IHttpClient<user>), x => new HttpClientBase<user>(usersUrl))
                .AddTransient(typeof(IHttpClient<album>), x => new HttpClientBase<album>(albumsUrl))
                .AddTransient(typeof(IHttpClient<photo>), x => new HttpClientBase<photo>(photosUrl))
                .AddTransient(typeof(IHttpClient<post>), x => new HttpClientBase<post>(postsUrl));
        }
        private static IServiceCollection addHttpJsonRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepository<user>), typeof(HttpJsonRepository<user>))
                .AddTransient(typeof(IRepository<album>), typeof(HttpJsonRepository<album>))
                .AddTransient(typeof(IRepository<photo>), typeof(HttpJsonRepository<photo>))
                .AddTransient(typeof(IRepository<post>), typeof(HttpJsonRepository<post>));
        }
        private static IServiceCollection addModelServices(this IServiceCollection services)
        {
            return services
                .AddTransient<UsersService, UsersService>()
                .AddTransient<AlbumsService, AlbumsService>()
                .AddTransient<PhotosService, PhotosService>()
                .AddTransient<PostsService, PostsService>()
                .AddTransient<CompleteAlbumsService, CompleteAlbumsService>();
        }
        private static IServiceCollection runStartupTasks(this IServiceCollection services)
        {
            services
                .Where(s => s.ServiceType.HasInterface<IStartupTask>())
                .Select(s => 
                    services.GetRequiredScopedService(s.ServiceType).As<IStartupTask>()
                ).Each(t => t.OnStartup());

            return services;
        }
    }
}