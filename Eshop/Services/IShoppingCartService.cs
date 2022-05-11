using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public interface IShoppingCartService
    {
        Task AddToCart(int id);
        Task<IEnumerable<ShoppingCartModel>> GetUserCartItems();
        Task GetUserCartItemByPostId(int? id);
        Task DeleteItemFromCart(int id);
    }
}