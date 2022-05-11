using Eshop.DTO;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class CommentViewModel
    {
        public AppUser LoggedUser { get; set; }
        public AppUser User { get; set; }
        public List<CommentModel> Comments { get; set; }
        public CreateCommentDTO CreateCommentDTO { get; set; }
        public SoldPostModel OfferBought { get; set; }
        public string Avg { get; set; }
        public string ComCount { get; set; }
    }
}
