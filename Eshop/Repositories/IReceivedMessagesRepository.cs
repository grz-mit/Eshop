using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IReceivedMessagesRepository
    {
        Task<ReceivedMessageModel> GetUserReceivedMessageById(int? id);
        Task<List<ReceivedMessageModel>> GetUserReceivedMessages();
        Task Create(ReceivedMessageModel receivedMessage);
    }
}