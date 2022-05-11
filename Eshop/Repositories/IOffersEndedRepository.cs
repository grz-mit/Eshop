using Eshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Repositories
{
    public interface IOffersEndedRepository
    {
        Task<List<SoldPostModel>> GetBoughtItemsByUser();
        Task<List<SoldPostModel>> GetSoldItemsByUserId();

        Task<SoldPostModel> GetBoughtOffer(int mainOfferId);
    }
}