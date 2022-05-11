using Eshop.Models;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public interface IUserService
    {
        Task DepositMoney(decimal money);
        Task<AppUser> GetUserById();
        Task<AppUser> GetUserById(string userId);
    }
}