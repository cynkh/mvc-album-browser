using System.Collections.Generic;
using System.Linq;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Models;

namespace mvc_album_browser.Services
{
    public class CompleteAlbumsService
    {
        private readonly AlbumsService _albumsService;
        private readonly UsersService _usersService;
        private readonly PhotosService _photosService;
        private readonly PostsService _postsService;
        public CompleteAlbumsService(AlbumsService albumsService, 
            UsersService usersService, 
            PhotosService photosService, 
            PostsService postsService)
        {
            _albumsService = albumsService;
            _photosService = photosService;
            _postsService = postsService;
            _usersService = usersService;
        }
        public IEnumerable<Album> GetAllAlbums()
        {
            var albums = _albumsService.GetAllAlbums();
            var photos = _photosService.GetAllPhotos();
            var users = _usersService.GetAllUsers();

            return albums.Select(a => {
                var user = users.FirstOrDefault(u => u.Id == a.userId);
                var photosForAlbum = photos.Where(p => p.albumId == a.id);

                return _createAlbumWithUserAndPhotos(a, user, photosForAlbum);
            });
        }
        public Album GetAlbumByAlbumId(int albumId)
        {
            var album = _albumsService.GetAlbumById(albumId);
            var photos = _photosService.GetAllPhotosByAlbumId(albumId);
            var user = _usersService.GetFirstUserById(album.userId);

            return _createAlbumWithUserAndPhotos(album, user, photos);
        }
        public IEnumerable<Album> FindAlbumsWhereTitleContains(string title, bool caseSensitive = false)
        {
            var albums = _albumsService.FindAlbumsWhereTitleContains(title, caseSensitive);
            var photos = _photosService.GetAllPhotosByAlbumIds(albums.Select(a => a.id));
            var users = _usersService.GetUsersByIds(albums.Select(a => a.userId));

            return albums.Select(a => {
                var user = users.FirstOrDefault(u => u.Id == a.userId);
                var photosForAlbum = photos.Where(p => p.albumId == a.id);

                return _createAlbumWithUserAndPhotos(a, user, photosForAlbum);
            });
        }
        // disambiguating between user name and user's name
        public IEnumerable<Album> FindAlbumsWhoseUsersNameContains(string name, bool caseSensitive = false)
        {
            var users = _usersService.FindUsersWhoseNamesContain(name, caseSensitive);
            var albums = _albumsService.FindAlbumsByUserIds(users.Select(u => u.Id));
            var photos = _photosService.GetAllPhotos();

            return albums.Select(a => {
                var user = users.FirstOrDefault(u => u.Id == a.userId);
                var photosForAlbum = photos.Where(p => p.albumId == a.id);

                return _createAlbumWithUserAndPhotos(a, user, photosForAlbum);
            });
        }
        public IEnumerable<Album> FindAlbumsWhereTitleOrUsersNameContains(string title = "", string name = "", bool caseSensitive = false)
        {
            IEnumerable<Album> albums;

            // find albums solely by title
            if (title != null && title.Trim().Length > 0 && 
                (name == null || name.Trim().Length < 1))
                {
                    albums = FindAlbumsWhereTitleContains(title, caseSensitive);
                }
            // find albums solely by name
            else if ((title == null || title.Trim().Length < 1) &&
                name != null && name.Trim().Length > 0)
                {
                    albums = FindAlbumsWhoseUsersNameContains(name, caseSensitive);
                }
            else if ((title == null || title.Trim().Length < 1) && 
                (name == null || name.Trim().Length < 1))
                {
                    albums = GetAllAlbums();
                }
            else  // find by both album title and user's name, based on selected operation
            {
                var filteredUsers = _usersService.FindUsersWhoseNamesContain(name, caseSensitive);

                var allAlbums = _albumsService
                    .FindAlbumsWhereTitleContains(title, caseSensitive)
                    .Concat(_albumsService.FindAlbumsByUserIds(filteredUsers.Select(u => u.Id)))
                    .Distinct();

                var allPhotos = _photosService.GetAllPhotos();
                var allUsers = _usersService.GetAllUsers();

                albums = allAlbums.Select(a => {
                    var user = allUsers.FirstOrDefault(u => u.Id == a.userId);
                    var photos = allPhotos.Where(p => p.albumId == a.id);

                    return _createAlbumWithUserAndPhotos(a, user, photos);
                });
            }

            return albums;
        }
        public Album FindFirstAlbumByExactTitle(string title)
        {
            var album = _albumsService.FindFirstAlbumByExactTitle(title);
            var photos = _photosService.GetAllPhotosByAlbumId(album.id);
            var user = _usersService.GetFirstUserById(album.userId);

            return _createAlbumWithUserAndPhotos(album, user, photos);
        }
        private Album _createAlbumWithUserAndPhotos(album album, User user, IEnumerable<photo> photos)
        {
            return new Album
            {
                Id = album.id,
                Title = album.title,
                User = user,
                Photos = photos.Select(p => p.MapTo<photo, Photo>())

                // Photos = photos.Select(p => new Photo
                // {
                //     Id = p.id,
                //     AlbumId = p.albumId,
                //     Title = p.title,
                //     Url = p.url,
                //     ThumbnailUrl = p.thumbnailUrl,
                // })
            };
        }
    }
}