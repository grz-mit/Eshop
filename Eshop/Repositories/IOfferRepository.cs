using Eshop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IOfferRepository
    {
        IQueryable<OfferModel> GetAll();
        Task<OfferModel> GetById(int? id);
        Task<List<OfferModel>> OwnedByUser();
        Task DeleteOffer(OfferModel offer);
        Task CreateOffer(OfferModel offer);
    }
}