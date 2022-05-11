using Eshop.Models;
using Eshop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public interface IMessagesService
    {
        Task<MessageViewModel> GetMessageVM(int id);
        Task CreateMessage(MessageViewModel messageVM);
        Task<List<SentMessageModel>> GetUserSentMessages();
        Task<SentMessageModel> GetUserSentMessageById(int? id);
        Task<List<ReceivedMessageModel>> GetUserReceivedMessages();
        Task<ReceivedMessageModel> GetUserReceivedMessageById(int? id);
        Task CreateReply(ReceivedMessageModel receivedMessage);
    }
}