using Eshop.DTO;
using Eshop.Models;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Eshop.Services
{
    public interface IOfferService
    {
        IQueryable<OfferModel> FilteredOffers(string searchString, string offerCategory, decimal? offerPriceFrom, decimal? offerPriceTo);
        Task<OffersViewModel> PaginatedOffers(IQueryable<OfferModel> filteredOffers, int page);
        Task<SelectList> Categories();
        Task<OfferDetailsViewModel> GetDetailsOfferVM(int? id);
        Task<OfferModel> OfferToDelete(int? id);
        Task DeleteOffer(int id);
        Task<BuyViewModel> OfferToBuy(int? id);
        Task BuyOffer(int id, OfferEndedModel offerEnded);
        Task CreateOffer(CreateOfferDTO createOfferDTO);
        Task<List<OfferModel>> OffersOwnedByUser();
    }
}