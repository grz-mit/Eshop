using Eshop.Data;
using Eshop.Models;
using Eshop.Services;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eshop.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessagesService _messageService;

        public MessagesController(IMessagesService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> CreateMessage(int id)
        {
            var messageVM = await _messageService.GetMessageVM(id);

            return View(messageVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(MessageViewModel messageVM)
        {
            await _messageService.CreateMessage(messageVM);
            return Redirect("/UserDetailsPost/Index/" + messageVM.MessageDTO.OfferOwnerId);
        }

        public async Task<IActionResult> Sent()
        {
            var sentMessages = await _messageService.GetUserSentMessages();
            
            return View(sentMessages);
        }

        public async Task<IActionResult> SentDetails(int? id)
        {
            var sentDetails = await _messageService.GetUserSentMessageById(id);

            return View(sentDetails);
        }

        public async Task<IActionResult> Received()
        {
            var receivedMessages = await _messageService.GetUserReceivedMessages();

            return View(receivedMessages);
        }

        public async Task<IActionResult> ReceivedDetails(int? id)
        {
            var receivedDetails = await _messageService.GetUserReceivedMessageById(id);

            return View(receivedDetails);
        }

        public async Task<IActionResult> Reply(ReceivedMessageModel replyMessage)
        {
            await _messageService.CreateReply(replyMessage);
            return Redirect("/Posts");
        }
    }
}
