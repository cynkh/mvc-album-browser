using System.Collections.Generic;
using System.Linq;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;

namespace mvc_album_browser.Services
{
    public class AlbumsService
    {
        private readonly IRepository<album> _repository;
        public AlbumsService(IRepository<album> repository)
        {
            _repository = repository;
        }
        public IEnumerable<album> GetAllAlbums()
        {
            return _repository.GetAll();
        }
        public album GetAlbumById(int id)
        {
            return _repository.FirstOrDefault(a => a.id == id);
        }
        public IEnumerable<album> FindAlbumsWhereTitleContains(string title, bool caseSensitive = false)
        {
            return _repository.Where(a => caseSensitive ? 
                a.title.Contains(title) : 
                a.title.CaseInsensitiveContains(title));
        }
        public album FindFirstAlbumByExactTitle(string title)
        {
            return _repository.FirstOrDefault(a => a.title == title);
        }
        public album FindFirstAlbumWhereTitleContains(string title, bool caseSensitive = false)
        {
            return _repository.FirstOrDefault(a => caseSensitive ? 
                a.title.Contains(title) : 
                a.title.CaseInsensitiveContains(title));
        }
        public IEnumerable<album> FindAlbumsByUserId(int userId)
        {
            return _repository.Where(a => a.userId == userId);
        }
        public IEnumerable<album> FindAlbumsByUserIds(IEnumerable<int> userIds)
        {
            return _repository.Where(a => userIds.Contains(a.userId));
        }
    }
}