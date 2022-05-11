using Eshop.Models;
using Eshop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class OffersEndedService : IOffersEndedService
    {
        private readonly IOffersEndedRepository _offersEndedRepository;

        public OffersEndedService(IOffersEndedRepository offersEndedRepository)
        {
            _offersEndedRepository = offersEndedRepository;
        }

        public async Task<List<SoldPostModel>> GetSoldItemsByUserId()
        {
            var itemsSold = await _offersEndedRepository.GetSoldItemsByUserId();

            return itemsSold;
        }

        public async Task<List<SoldPostModel>> GetBoughtItemsByUserId()
        {
            var itemsBought = await _offersEndedRepository.GetBoughtItemsByUser();

            return itemsBought;
        }

        public async Task<SoldPostModel> GetBoughtOffer(int mainOfferId)
        {
            var itemBought = await _offersEndedRepository.GetBoughtOffer(mainOfferId);

            return itemBought;
        }

    }
}
