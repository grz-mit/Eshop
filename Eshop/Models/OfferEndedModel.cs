using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class OfferEndedModel
    {
        public int Id { get; set; }
        public string UserWhoBought { get;set; } //id kupujacego
        public string UserId { get; set; } //id wlasciciela   
        public string Title { get; set; }
        public string Content { get; set; }
        
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public DateTime SoldDate { get; set; }
        public string Thumbnail { get; set; }

        public ShippingInformationModel ShippingInformation { get; set; }
        public CommentModel Comment { get; set; }
        public AppUser User { get; set; }
    }
}
