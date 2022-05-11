using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Eshop.Models;
using Microsoft.AspNetCore.Authorization;
using Eshop.ViewModels;
using Eshop.Services;
using Eshop.DTO;

namespace Eshop.Controllers
{
    public class PostsController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPostService _postService;
        private readonly IUserContextService _userContextService;

        public PostsController(IWebHostEnvironment hostEnvironment, IPostService postService, IUserContextService userContextService)
        {
            _hostEnvironment = hostEnvironment;
            _postService = postService;
            _userContextService = userContextService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string postCategory, decimal postPriceFrom, decimal postPriceTo)
        {
            var postGenreVM = new CategoryModel
            {
                Genres = await _postService.Categories(),
                Posts = await _postService.FilteredPosts(searchString, postCategory, postPriceFrom, postPriceTo)
            };

            return View(postGenreVM);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var postDetailsVM = await _postService.GetDetailPostVM(id);

            return View(postDetailsVM);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostDTO postDTO)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreatePost(postDTO);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var post = await _postService.PostToDelete(id);

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Buy(int? id)
        {
            var buyVM = await _postService.PostToBuy(id);

            return View(buyVM);
        }
        //dodac automappera ktory zmauje na sold post model posta 
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyConfirmed(int id, SoldPostModel soldPost)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Buy));
            }

            await _postService.BuyPost(id, soldPost);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Owned()
        {
            var userOffers = await _postService.OffersOwnedByUser();
            return View(userOffers);
        }
    }
}
