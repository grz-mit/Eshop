using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public interface IOffersEndedService
    {
        Task<List<OfferEndedModel>> GetBoughtItemsByUserId();
        Task<List<OfferEndedModel>> GetSoldItemsByUserId();
        Task<OfferEndedModel> GetBoughtOffer(int mainOfferId);
    }
}