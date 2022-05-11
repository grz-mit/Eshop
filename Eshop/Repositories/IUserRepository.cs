using Eshop.Models;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser> GetById();
        Task<AppUser> GetById(string id);
        Task UpdateUser(AppUser user);
    }
}