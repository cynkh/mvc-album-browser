using Microsoft.AspNetCore.Mvc;

using mvc_album_browser.Models;
using mvc_album_browser.Services;

namespace mvc_album_browser.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostsService _postsService;
        private readonly UsersService _usersService;
        public PostsController(PostsService postsService, UsersService usersService)
        {
            _postsService = postsService;
            _usersService = usersService;
        }
        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }

            var posts = _postsService.GetAllPosts();

            var model = new PostsIndexViewModel
            {
                Posts = posts
            };

            return View(model);
        }
        public IActionResult ByUser(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var user = _usersService.GetFirstUserById(id.Value);
            var posts = _postsService.GetAllPostsByUserId(id.Value);

            var model = new PostsByUserViewModel
            {
                User = user,
                Posts = posts
            };

            return View();
        }
    }
}