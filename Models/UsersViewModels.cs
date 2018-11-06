using System.Collections.Generic;

namespace mvc_album_browser.Models
{
    public class UsersIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
    public class UsersDetailsViewModel
    {
        public User User { get; set; }
    }
}