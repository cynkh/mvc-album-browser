using mvc_album_browser.Entities;
using mvc_album_browser.Models;

namespace mvc_album_browser
{
    public class MapperConfigurationStartupTask : IStartupTask
    {
        private readonly IMapperConfigurator _mapperConfigurator;
        public MapperConfigurationStartupTask()
        {
            _mapperConfigurator = MapperConfiguratorProvider.GetMapperConfigurator();
        }
        public void OnStartup()
        {
            _setMappers();
        }
        private void _setMappers()
        {
            _mapperConfigurator
                .SetMapper<post, Post>(post => {
                    return new Post
                    {
                        Id = post.id,
                        UserId = post.userId,
                        Title = post.title,
                        Body = post.body
                    };
                })
                .SetMapper<photo, Photo>(photo => {
                    return new Photo
                    {
                        Id = photo.id,
                        AlbumId = photo.albumId,
                        Title = photo.title,
                        Url = photo.url,
                        ThumbnailUrl = photo.thumbnailUrl,
                    };
                })
                .SetMapper<geo, GeoLocation>(geo => {
                    return new GeoLocation
                    {
                        Latitude = geo.lat,
                        Longitude = geo.lng
                    };
                })
                .SetMapper<address, Address>(address => {
                    return new Address
                    {
                        Street = address.street,
                        Suite = address.suite,
                        City = address.city,
                        ZipCode = address.zipcode,
                        Geo = address.geo?.MapTo<geo, GeoLocation>()
                    };
                })
                .SetMapper<user, User>(user => {
                    return new User
                    {
                        Id = user.id,
                        Name = user.name,
                        UserName = user.username,
                        PhoneNumber = user.phone,
                        EmailAddress = user.email,
                        Address = user.address?.MapTo<address, Address>()
                    };
                });
        }
    }
}