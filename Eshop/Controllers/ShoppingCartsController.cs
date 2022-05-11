using Eshop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Eshop.Services;

namespace Eshop.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IWebHostEnvironment hostEnvironment)
        {
            _shoppingCartService = shoppingCartService;
            _hostEnvironment = hostEnvironment; 
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            await _shoppingCartService.AddToCart(id);
            
            return Redirect("/posts/details/" + id);
        }

        public async Task<IActionResult> Index()
        {
            var userCart = await _shoppingCartService.GetUserCartItems();
            
            return View(userCart);     
        }

        public async Task<IActionResult> Delete(int? id)
        {
            await _shoppingCartService.GetUserCartItemByPostId(id);

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _shoppingCartService.DeleteItemFromCart(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
