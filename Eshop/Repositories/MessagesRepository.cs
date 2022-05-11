using Eshop.Data;
using Eshop.Models;
using Eshop.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public MessagesRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Create(SentMessageModel sentMessage)
        {
            await _context.SentMessages.AddAsync(sentMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SentMessageModel>> GetUserSentMessages()
        {
            var result = await _context.SentMessages
                                         .Where(p => p.UserId == _userContextService.UserId)
                                         .ToListAsync();

            return result;
        }

        public async Task<SentMessageModel> GetUserSentMessageById(int? id)
        {
            var result = await _context.SentMessages.FirstOrDefaultAsync(p => p.UserId == _userContextService.UserId && p.Id == id);
            
            return result;
        }

    }
}
