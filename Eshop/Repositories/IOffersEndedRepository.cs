using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IOffersEndedRepository
    {
        Task<List<OfferEndedModel>> GetBoughtItemsByUser();
        Task<List<OfferEndedModel>> GetSoldItemsByUserId();
        Task CreateOfferEnded(OfferEndedModel offerEnded);
        Task<OfferEndedModel> GetBoughtOffer(int mainOfferId);
    }
}