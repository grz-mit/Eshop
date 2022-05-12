using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class OfferDetailsViewModel
    {
        public OfferModel Offer { get; set; }
        public List<OfferModel> OtherOffers { get; set; }
        public bool InCart { get; set; }   
        public string UserId { get; set; }
    }
}
