using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class CommentsViewModel
    {
        public AppUser LoggedUser { get; set; }
        public AppUser User { get; set; }
        public List<CommentModel> Comments { get; set; }
        public ReplyComModel Reply { get; set; }

        public string Avg { get; set; }
        public string ComCount { get; set; }
        public int? OfferId { get; set; }
        public string OfferOwnerId { get; set; }

    }
}
