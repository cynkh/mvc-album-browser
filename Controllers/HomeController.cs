using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Services;

namespace mvc_album_browser.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersService _usersService;
        private readonly AlbumsService _albumsService;
        public HomeController(UsersService usersService, AlbumsService albumsService)
        {
            _usersService = usersService;
            _albumsService = albumsService;
        }
        public IActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Albums");
        }

        public IActionResult About(string name, string title)
        {
            var users = name == null || name.Trim().Length < 1 ?
                _usersService.GetAllUsers() : 
                _usersService.FindUsersWhoseNamesContain(name);

            var albums = title == null || title.Trim().Length < 1 ? 
                _albumsService.GetAllAlbums() : 
                _albumsService.FindAlbumsWhereTitleContains(title);

            ViewData["Message"] = "Your application description page.";
            ViewData["Users"] = users;
            ViewData["Albums"] = albums;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
