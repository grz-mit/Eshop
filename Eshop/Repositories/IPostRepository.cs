using Eshop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IPostRepository
    {
        IQueryable<PostModel> GetAll();
        Task<PostModel> GetById(int? id);
        Task<List<PostModel>> OwnedByUser();
        Task DeletePost(PostModel post);
        Task CreatePost(PostModel post);
    }
}