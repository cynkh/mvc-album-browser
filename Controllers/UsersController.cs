using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using mvc_album_browser.Models;
using mvc_album_browser.Services;

namespace mvc_album_browser.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;
        private readonly PostsService _postsService;
        public UsersController(UsersService usersService, PostsService postsService)
        {
            _usersService = usersService;
            _postsService = postsService;
        }
        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                return RedirectToAction("Details", id.Value);
            }

            var users = _usersService.GetAllUsers();
            var model = new UsersIndexViewModel
            {
                Users = users
            };

            return View(model);
        }
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var user = _usersService.GetFirstUserById(id.Value);
            var posts = _postsService.GetAllPostsByUserId(id.Value);
            user.Posts = posts;

            var model = new UsersDetailsViewModel
            {
                User = user
            };

            return View(model);
        }
    }
}