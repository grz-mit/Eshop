using AutoMapper;
using Eshop.DTO;
using Eshop.Exceptions;
using Eshop.Models;
using Eshop.Repositories;
using Eshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IReceivedMessagesRepository _receivedMessagesRepository;

        public MessagesService(IOfferRepository offerRepository, IUserRepository userRepository,
            IUserContextService userContextService, IMapper mapper, IMessagesRepository messagesRepository, IReceivedMessagesRepository receivedMessagesRepository)
        {
            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _userContextService = userContextService;
            _mapper = mapper;
            _messagesRepository = messagesRepository;
            _receivedMessagesRepository = receivedMessagesRepository;
        }

        public async Task<MessageViewModel> GetMessageVM(int id)
        {
            var offer = await _offerRepository.GetById(id);

            if (offer == null)
                throw new NotFoundException();

            if (offer.User.Id == _userContextService.UserId)
                throw new UnauthorizedAccessException();

            return new MessageViewModel()
            {
                UserName = offer.User.NickName,
                OfferTitle = offer.Title,
                
                MessageDTO = new MessageDTO()
                {
                    ReceiverName = offer.User.NickName,
                    OfferOwnerId = offer.UserId,
                    UserId = _userContextService.UserId
                }
            };
        }

        public async Task CreateMessage(MessageViewModel messageVM)
        {
            var user = await _userRepository.GetById(_userContextService.UserId);

            var sentMessage = _mapper.Map<SentMessageModel>(messageVM.MessageDTO);
            sentMessage.SentDate = DateTime.Now;
                     
            var receivedMessage = _mapper.Map<ReceivedMessageModel>(messageVM.MessageDTO);
            receivedMessage.SentDate = DateTime.Now;
            receivedMessage.SenderName = user.NickName;

            await _messagesRepository.Create(sentMessage);
            await _receivedMessagesRepository.Create(receivedMessage);
        }

        public async Task<List<SentMessageModel>> GetUserSentMessages()
        {
            var sentMessages = await _messagesRepository.GetUserSentMessages();
            sentMessages.Reverse();

            return sentMessages;
        }

        public async Task<SentMessageModel> GetUserSentMessageById(int? id)
        {
            var sentMessage = await _messagesRepository.GetUserSentMessageById(id);

            if (sentMessage == null)
                throw new NotFoundException();

            return sentMessage;
        }

        public async Task<List<ReceivedMessageModel>> GetUserReceivedMessages()
        {
            var receivedMessages = await _receivedMessagesRepository.GetUserReceivedMessages();
            receivedMessages.Reverse();

            return receivedMessages;
        }

        public async Task<ReceivedMessageModel> GetUserReceivedMessageById(int? id)
        {
            var receivedMessage = await _receivedMessagesRepository.GetUserReceivedMessageById(id);

            if (receivedMessage == null)
                throw new NotFoundException();

            return receivedMessage;
        }

        public async Task CreateReply(ReceivedMessageModel receivedMessage)
        {
            var sentDate = DateTime.Now;
            var userSending = await _userRepository.GetById(_userContextService.UserId);
            var userReceiving = await _userRepository.GetById(receivedMessage.UserId);

            var sentMessage = new SentMessageModel()
            {
                UserId = _userContextService.UserId,
                MessageTitle = receivedMessage.MessageTitle,
                MessageContent = receivedMessage.MessageContent,
                ReceiverName = userReceiving.NickName,
                OfferTitle = receivedMessage.OfferTitle,
                SentDate = sentDate
            };

            receivedMessage.SenderName = userSending.NickName;
            receivedMessage.ReplyId = _userContextService.UserId;
            receivedMessage.SentDate = sentDate;

            await _messagesRepository.Create(sentMessage);
            await _receivedMessagesRepository.Create(receivedMessage);
        }

    }
}
