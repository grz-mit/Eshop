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
    public class ReceivedMessagesRepository : IReceivedMessagesRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public ReceivedMessagesRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Create(ReceivedMessageModel receivedMessage)
        {
            await _context.ReceivedMessages.AddAsync(receivedMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReceivedMessageModel>> GetUserReceivedMessages()
        {
            var result = await _context.ReceivedMessages
                                         .Where(p => p.UserId == _userContextService.UserId)
                                         .ToListAsync();

            return result;
        }

        public async Task<ReceivedMessageModel> GetUserReceivedMessageById(int? id)
        {
            var result = await _context.ReceivedMessages.FirstOrDefaultAsync(p => p.UserId == _userContextService.UserId && p.Id == id);

            return result;
        }
    }
}
