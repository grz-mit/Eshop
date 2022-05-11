using Eshop.Data;
using Eshop.Models;
using Eshop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public ShoppingCartRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }


        public async Task <List<ShoppingCartModel>> GetUserCartItems()
        {
            var result =  await _context.ShoppingCarts.Where(p => p.CartId == _userContextService.UserId)
                                                      .Include(p => p.Post)
                                                      .Include(p => p.Post.GalleryModel.ImageModel)    
                                                      .ToListAsync();
            return result;
        }

        public async Task AddItemToCart(ShoppingCartModel cartItem)
        {
            await _context.ShoppingCarts.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemFromCart(ShoppingCartModel cartItem)
        {
            _context.ShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

    }
}
