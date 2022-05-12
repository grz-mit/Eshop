using Eshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class OfferModel
    {
        public int Id { get; set; }
       
        public string UserId { get; set; }

        [Required(ErrorMessage ="To pole jest wymagane")]
        [StringLength(100, ErrorMessage = "Tytuł może zawierać maksymalnie {1} znaków")]
        public string Title { get; set; }

        [StringLength(4096, ErrorMessage = "Opis może zawierać maksymalnie {1} znaków")]
        public string Content { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0,999999.99,ErrorMessage ="Cena musi znajdować sie w przedziale od {1} do {2} PLN")]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public DateTime CreateDate { get; set; }

        [DisplayName]
        public GalleryModel GalleryModel { get; set; }
       
        public AppUser User { get; set; }
        
        [NotMapped]
        public SoldPostModel SoldPost { get; set; }

        [NotMapped]
        public SentMessageModel SentMessage { get; set; }

    }
}
