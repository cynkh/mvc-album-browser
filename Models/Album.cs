using System.Collections.Generic;

namespace mvc_album_browser.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
    public class Photo
    {
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
    }
}