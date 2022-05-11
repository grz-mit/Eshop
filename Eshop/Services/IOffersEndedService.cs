using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public interface IOffersEndedService
    {
        Task<List<SoldPostModel>> GetBoughtItemsByUserId();
        Task<List<SoldPostModel>> GetSoldItemsByUserId();
        Task<SoldPostModel> GetBoughtOffer(int mainOfferId);
    }
}