using Eshop.DTO;
using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public interface ICommentService
    {
        string CalculateAvgRating(List<CommentModel> userComments);
        Task<List<CommentModel>> GetReceivedComments();
        Task<List<CommentModel>> GetReceivedComments(string userId);
        Task CreateReply(ReplyComModel reply);
        Task CreateComment(CreateCommentDTO createCommentDTO);
    }
}