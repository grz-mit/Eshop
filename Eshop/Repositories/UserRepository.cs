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
    public class UserRepository : IUserRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public UserRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<AppUser> GetById()
        {
            var result = await _context.Users.FirstOrDefaultAsync(p => p.Id == _userContextService.UserId);
            return result;
        }
        public async Task<AppUser> GetById(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task UpdateUser(AppUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();        
        }
    }
}
