using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class GalleryModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public List<ImageModel> ImageModel { get; set; }
        public OfferModel Offer { get; set; }

    }
}
