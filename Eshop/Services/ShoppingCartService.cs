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
        private readonly IOfferRepository _offerRepository;
        private readonly IUserContextService _userContextService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IOfferRepository offerRepository, IUserContextService userContextService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _offerRepository = offerRepository;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<ShoppingCartModel>> GetUserCartItems()
        {
            var cartItems = await _shoppingCartRepository.GetUserCartItems();    

            return cartItems;
        }

        public async Task AddToCart(int id)
        {
            var offer = await _offerRepository.GetById(id);

            if (offer.UserId != _userContextService.UserId)
            {
                var cartItems = await _shoppingCartRepository.GetUserCartItems();

                if (cartItems.Any(p => p.OfferId == id))
                {
                    throw new ForbiddenActionException();
                }

                var cartItem = new ShoppingCartModel
                {
                    OfferId = id,
                    CartId = _userContextService.UserId
                };

                await _shoppingCartRepository.AddItemToCart(cartItem);

            }
            else
            {
                throw new ForbiddenActionException();
            }
        }

        public async Task GetUserCartItemByOfferId(int? id)
        {
            var cartItems = await _shoppingCartRepository.GetUserCartItems();
            var item = cartItems.FirstOrDefault(s => s.OfferId == id);

            if (item == null)
            {
                throw new NotFoundException();
            }
        }

        public async Task DeleteItemFromCart(int id)
        {
            var cartItems = await _shoppingCartRepository.GetUserCartItems();
            var item = cartItems.FirstOrDefault(s => s.OfferId == id);

            await _shoppingCartRepository.DeleteItemFromCart(item);
        }
    }
}
