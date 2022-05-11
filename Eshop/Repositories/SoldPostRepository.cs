using Eshop.Data;
using Eshop.Exceptions;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public class SoldPostRepository : ISoldPostRepository
    {
        private readonly EshopDbContext _context;

        public SoldPostRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task CreateSoldPost(SoldPostModel soldPost)
        {
            await _context.SoldPosts.AddAsync(soldPost);
            await _context.SaveChangesAsync();   
        }
    }
}
