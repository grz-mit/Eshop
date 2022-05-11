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
    public class CommentRepository : ICommentRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public CommentRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<CommentModel> GetCommentById(int id)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(p => p.Id == id);
            
            return result;
        }

        public async Task<List<CommentModel>> GetReceivedComments()
        {
            var result = await _context.Comments.Where(p => p.PostUserId == _userContextService.UserId)
                                                .Include(p=>p.Reply)
                                                .ToListAsync();

            return result;
        }

        public async Task<List<CommentModel>> GetReceivedComments(string id)
        {
            var result = await _context.Comments.Where(p => p.PostUserId == id)
                                                .Include(p => p.Reply)
                                                .ToListAsync();

            return result;
        }

        public async Task CreateComment(CommentModel commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
        }

    }
}
