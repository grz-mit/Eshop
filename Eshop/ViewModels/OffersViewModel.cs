using Eshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class OffersViewModel
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public string HasPreviousPage { get; set; }
        public string HasNextPage { get; set; }
        public List<OfferModel> Offers { get; set; }
        public SelectList Genres { get; set; }
    }
}
