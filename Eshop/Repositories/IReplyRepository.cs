using Eshop.Models;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IReplyRepository
    {
        Task CreateReply(ReplyComModel reply);
    }
}