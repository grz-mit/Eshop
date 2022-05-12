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
    public class OfferRepository : IOfferRepository
    {
        private readonly EshopDbContext _context;
        private readonly IUserContextService _userContextService;

        public OfferRepository(EshopDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public IQueryable<OfferModel> GetAll()
        {
            var results = _context.Offers
                                  .Include(s => s.GalleryModel.ImageModel)
                                  .AsQueryable();

            return results;
        }

        public async Task<OfferModel> GetById(int? id)
        {
            var result = await _context.Offers.Include(s => s.GalleryModel.ImageModel)
                                              .Include(p => p.User)
                                              .FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }
        public async Task<List<OfferModel>> OwnedByUser()
        {
            var results = await _context.Offers
                                        .Include(p => p.GalleryModel.ImageModel)
                                        .Where(p => p.UserId == _userContextService.UserId)
                                        .ToListAsync();

            return results;
        }

        public async Task DeleteOffer(OfferModel offer)
        {
            _context.Remove(offer);
            await _context.SaveChangesAsync();
        }

        public async Task CreateOffer(OfferModel offer)
        {
            await _context.AddAsync(offer);
            await _context.SaveChangesAsync();
        }
    }
}
