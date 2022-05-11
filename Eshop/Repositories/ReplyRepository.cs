using Eshop.Data;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly EshopDbContext _context;

        public ReplyRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task CreateReply(ReplyComModel reply)
        {
            await _context.Reply.AddAsync(reply);
            await _context.SaveChangesAsync();
        }
    }
}
