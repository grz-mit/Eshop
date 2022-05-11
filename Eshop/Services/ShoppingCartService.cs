using Eshop.Exceptions;
using Eshop.Models;
using Eshop.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserContextService _userContextService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IPostRepository postRepository, IUserContextService userContextService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _postRepository = postRepository;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<ShoppingCartModel>> GetUserCartItems()
        {
            var cartItems = await _shoppingCartRepository.GetUserCartItems();    

            return cartItems;
        }

        public async Task AddToCart(int id)
        {
            var post = await _postRepository.GetById(id);

            if (post.UserId != _userContextService.UserId)
            {
                var cartItems = await _shoppingCartRepository.GetUserCartItems();

                if (cartItems.Any(p => p.PostId == id))
                {
                    throw new ForbiddenActionException();
                }

                var cartItem = new ShoppingCartModel
                {
                    PostId = id,
                    CartId = _userContextService.UserId
                };

                await _shoppingCartRepository.AddItemToCart(cartItem);

            }
            else
            {
                throw new ForbiddenActionException();
            }
        }

        public async Task GetUserCartItemByPostId(int? id)
        {
            var cartItems = await _shoppingCartRepository.GetUserCartItems();
            var item = cartItems.FirstOrDefault(s => s.PostId == id);

            if (item == null)
            {
                throw new NotFoundException();
            }
        }

        public async Task DeleteItemFromCart(int id)
        {
            var cartItems = await _shoppingCartRepository.GetUserCartItems();
            var item = cartItems.FirstOrDefault(s => s.PostId == id);

            await _shoppingCartRepository.DeleteItemFromCart(item);
        }
    }
}
