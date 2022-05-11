using Eshop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<List<ShoppingCartModel>> GetUserCartItems();
        Task AddItemToCart(ShoppingCartModel cartItem);
        Task DeleteItemFromCart(ShoppingCartModel cartItem);
    }
}