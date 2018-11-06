using System.Collections.Generic;
using System.Linq;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;

namespace mvc_album_browser.Services
{
    public class PhotosService
    {
        private readonly IRepository<photo> _repository;
        public PhotosService(IRepository<photo> repository)
        {
            _repository = repository;
        }
        public IEnumerable<photo> GetAllPhotos()
        {
            return _repository.GetAll();
        }
        public IEnumerable<photo> GetAllPhotosByAlbumId(int albumId)
        {
            return _repository.Where(p => p.albumId == albumId);
        }
        public IEnumerable<photo> GetAllPhotosByAlbumIds(IEnumerable<int> albumIds)
        {
            return _repository.Where(p => albumIds.Contains(p.albumId));
        }
        public photo GetFirstPhotoByAlbumId(int albumId)
        {
            return _repository.FirstOrDefault(p => p.albumId == albumId);
        }
    }
}