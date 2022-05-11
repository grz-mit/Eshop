using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IMessagesRepository
    {
        Task Create(SentMessageModel sentMessage);
        Task<List<SentMessageModel>> GetUserSentMessages();
        Task<SentMessageModel> GetUserSentMessageById(int? id);
    }
}