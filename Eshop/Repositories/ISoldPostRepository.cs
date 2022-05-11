using Eshop.Models;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface ISoldPostRepository
    {
        Task CreateSoldPost(SoldPostModel soldPost);
    }
}