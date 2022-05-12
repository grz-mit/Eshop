using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string OfferUserId { get; set; } 
        //public int PostId { get; set; } 
        //public string IdForReply { get; set; }
        public int SoldOfferId { get; set; }
        public string NickName { get; set; }
        public string Com { get; set; }
        public int Rate { get; set; }
        public string Title { get; set; }    
        public AppUser User { get; set; }
        public ReplyComModel Reply { get; set; }
        public SoldPostModel SoldOffer { get; set; }
    }
}
