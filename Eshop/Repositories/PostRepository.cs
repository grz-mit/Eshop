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
    public class PostRepository : IPostRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public PostRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public IQueryable<PostModel> GetAll()
        {
            var results = _context.Posts
                                  .Include(s => s.GalleryModel.ImageModel)
                                  .AsQueryable();

            return results;
        }

        public async Task<PostModel> GetById(int? id)
        {
            var result = await _context.Posts.Include(s => s.GalleryModel.ImageModel)
                                             .Include(p => p.User)
                                             .FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }
        public async Task<List<PostModel>> OwnedByUser()
        {
            var results = await _context.Posts
                                  .Include(p => p.GalleryModel.ImageModel)
                                  .Where(p => p.UserId == _userContextService.UserId)
                                  .ToListAsync();

            return results;
        }

        public async Task DeletePost(PostModel post)
        {
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePost(PostModel post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
        }
    }
}
