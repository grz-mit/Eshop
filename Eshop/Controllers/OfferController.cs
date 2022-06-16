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
    public class OfferController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IOfferService _postService;
        private readonly IUserContextService _userContextService;

        public OfferController(IWebHostEnvironment hostEnvironment, IOfferService postService, IUserContextService userContextService)
        {
            _hostEnvironment = hostEnvironment;
            _postService = postService;
            _userContextService = userContextService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string offerCategory, decimal? offerPriceFrom, decimal? offerPriceTo, int page)
        {
            ViewData["SearchString"] = searchString;
            ViewData["Category"] = offerCategory;
            ViewData["OfferPriceFrom"] = offerPriceFrom;
            ViewData["OfferPriceTo"] = offerPriceTo;

            var filteredOffers = _postService.FilteredOffers(searchString, offerCategory, offerPriceFrom, offerPriceTo).AsQueryable();
            var offersVM = await _postService.PaginatedOffers(filteredOffers, page);

            return View(offersVM);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var offerDetailVM = await _postService.GetDetailsOfferVM(id);

            return View(offerDetailVM);
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
        public async Task<IActionResult> Create(CreateOfferDTO offerDTO)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreateOffer(offerDTO);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var offer = await _postService.OfferToDelete(id);

            return View(offer);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeleteOffer(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Buy(int? id)
        {
            var buyVM = await _postService.OfferToBuy(id);

            return View(buyVM);
        }
        //dodac automappera ktory zmauje na sold post model posta 
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyConfirmed(int id, OfferEndedModel offerEnded)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Buy));
            }

            await _postService.BuyOffer(id, offerEnded);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Owned()
        {
            var userOffers = await _postService.OffersOwnedByUser();
            return View(userOffers);
        }
    }
}
