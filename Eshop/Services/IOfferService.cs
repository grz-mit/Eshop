using Eshop.DTO;
using Eshop.Models;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Eshop.Services
{
    public interface IOfferService
    {
        Task<List<OfferModel>> FilteredOffers(string searchString, string offerCategory, decimal offerPriceFrom, decimal offerPriceTo);
        Task<SelectList> Categories();
        Task<OfferDetailsViewModel> GetDetailsOfferVM(int? id);
        Task<OfferModel> OfferToDelete(int? id);
        Task DeleteOffer(int id);
        Task<BuyViewModel> OfferToBuy(int? id);
        Task BuyOffer(int id, SoldPostModel soldOffer);
        Task CreateOffer(CreateOfferDTO createPostDTO);
        Task<List<OfferModel>> OffersOwnedByUser();
    }
}