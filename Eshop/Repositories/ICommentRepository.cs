using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface ICommentRepository
    {
        Task<List<CommentModel>> GetReceivedComments();
        Task<List<CommentModel>> GetReceivedComments(string userId);
        Task<CommentModel> GetCommentById(int id);
        Task CreateComment(CommentModel commentModel);
    }
}