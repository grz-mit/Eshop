using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Models;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Models
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(8)")]
        public string NickName { get; set; }

        public DateTime CreateDate { get; set; }

        [Range(0, 999999.99, ErrorMessage = "Ilość musi znajdować sie w przedziale od {1} do {2} PLN")]
        public decimal Wallet { get; set; }

        public List<CommentModel> CommentModels { get; set; }
        [NotMapped]
        public CommentModel CommentModel { get; set; }
        public List<SentMessageModel> SentMessageModel { get; set; }
        public List<ReceivedMessageModel> ReceivedMessageModels { get; set; }
        public List<OfferModel> Offers { get; set; }
        public List<OfferEndedModel> SoldPosts { get; set; }
    }
}
