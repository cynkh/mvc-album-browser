using mvc_album_browser.Entities;
using mvc_album_browser.Models;

namespace mvc_album_browser
{
    public class MapperConfigurationStartupTask : IStartupTask
    {
        public void OnStartup()
        {
            _setMappers();
        }
        private MapperConfigurator _mapper
        {
            get
            {
                return MapperConfigurator.Mapper;
            }
        }
        private void _setMappers()
        {
            _mapper.SetMap<post, Post>(post => {
                return new Post
                {
                    Id = post.id,
                    UserId = post.userId,
                    Title = post.title,
                    Body = post.body
                };
            });

            _mapper.SetMap<photo, Photo>(photo => {
                return new Photo
                {
                    Id = photo.id,
                    AlbumId = photo.albumId,
                    Title = photo.title,
                    Url = photo.url,
                    ThumbnailUrl = photo.thumbnailUrl,
                };
            });

            _mapper.SetMap<geo, GeoLocation>(geo => {
                return new GeoLocation
                {
                    Latitude = geo.lat,
                    Longitude = geo.lng
                };
            });

            _mapper.SetMap<address, Address>(address => {
                var geo = address.geo;

                return new Address
                {
                    Street = address.street,
                    Suite = address.suite,
                    City = address.city,
                    ZipCode = address.zipcode,
                    Geo = geo.MapTo<geo, GeoLocation>()
                };
            });

            _mapper.SetMap<user, User>(user => {
                return new User
                {
                    Id = user.id,
                    Name = user.name,
                    UserName = user.username,
                    PhoneNumber = user.phone,
                    EmailAddress = user.email,
                    Address = user.address.MapTo<address, Address>()
                };
            });
        }
    }
}