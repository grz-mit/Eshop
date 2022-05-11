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
    public class OffersEndedRepository : IOffersEndedRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public OffersEndedRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<SoldPostModel>> GetSoldItemsByUserId()
        {
            var result = await _context.SoldPosts.Where(p => p.UserId == _userContextService.UserId)
                                                 .Include(p=>p.ShippingInformation)
                                                 .ToListAsync();

            return result;
        }

        public async Task<List<SoldPostModel>> GetBoughtItemsByUser()
        {
            var result = await _context.SoldPosts.Where(p => p.UserWhoBought == _userContextService.UserId)
                                                 .Include(p => p.User)
                                                 .Include(p => p.ShippingInformation)
                                                 .ToListAsync();

            return result;
        }

        public async Task<SoldPostModel> GetBoughtOffer(int mainOfferId)
        {
            var result = await _context.SoldPosts.Include(p => p.User)
                                                 .Include(p=>p.Comment)
                                                 .Include(p => p.ShippingInformation)
                                                 .FirstOrDefaultAsync(p => p.Id == mainOfferId);

            return result;
        }
    }
}
