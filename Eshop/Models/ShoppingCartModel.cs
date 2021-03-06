using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }

        public string CartId { get; set; }

        public int OfferId { get; set; }

        public OfferModel Offer { get; set; }
    }
}
