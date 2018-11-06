using System.Collections.Generic;

namespace mvc_album_browser.Models
{
    public class AlbumsIndexViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        public int TotalAlbumCount { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
        public SearchType SearchType { get; set; }
    }
}