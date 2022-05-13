using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class BuyViewModel
    {
        public OfferModel Offer { get; set; }
        public OfferEndedModel OfferEnded { get; set; }
        public decimal BuyerWallet { get; set; }
        public decimal WalletAfterBuy { get; set; }
    }
}
