using System.Collections.Generic;

namespace mvc_album_browser.Models
{
    public class PostsIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
    public class PostsByUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}