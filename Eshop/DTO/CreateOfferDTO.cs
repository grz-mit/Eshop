using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.DTO
{
    public class CreateOfferDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(100, ErrorMessage = "Tytuł może zawierać maksymalnie {1} znaków")]
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<IFormFile> ImageFile { get; set; }
    }
}
