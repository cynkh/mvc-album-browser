using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Mvc;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Models;
using mvc_album_browser.Services;

namespace mvc_album_browser.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly CompleteAlbumsService _completeAlbumsService;
        public AlbumsController(CompleteAlbumsService completeAlbumsService)
        {
            _completeAlbumsService = completeAlbumsService;
        }
        // leaning on the default routing scheme in the dotnet core mvc template
        public IActionResult Index(int? id, int? page = 0, string search = "", SearchType searchType = SearchType.Title)
        {
            if (id.HasValue)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }

            const int TAKE = 3;

            var pageNum = page ?? 0;

            IEnumerable<Album> albums;

            if (search != null && search.Trim().Length > 0)
            {
                albums = searchType.Equals(SearchType.Title) ?
                    _completeAlbumsService.FindAlbumsWhereTitleContains(search) : 
                    _completeAlbumsService.FindAlbumsWhoseUsersNameContains(search);
            }
            else
            {
                albums = _completeAlbumsService.GetAllAlbums();
            }

            var totalAlbumCount = albums.Count();
            var totalPages = (int)(totalAlbumCount / 3);

            if (pageNum < 0) pageNum = 0;
            if (pageNum > totalPages - 1) pageNum = totalPages - 1;

            if (albums.Count() % 3 != 0) totalPages++;

            albums = albums.Skip((pageNum) * TAKE);

            var model = new AlbumsIndexViewModel
            {
                Albums = albums.Take(TAKE),
                TotalAlbumCount = totalAlbumCount,
                Page = pageNum,
                TotalPages = totalPages,
                SearchType = searchType,
                Search = search,
            };

            return View(model);
        }
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var album = _completeAlbumsService.GetAlbumByAlbumId(id.Value);
            ViewData["Album"] = album;

            return View();
        }
    }
}