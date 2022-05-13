using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class ShippingInformationModel
    {
        public int Id { get; set; }
        public int OfferEndedId{ get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string City { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [RegularExpression(@"^[\d]{2}[-][\d]{3}$", ErrorMessage = "Niewłaściwy kod pocztowy. Prawidłowy format: xx-xxx.")]
        public string PostCode { get; set; }

        public OfferEndedModel OfferEnded { get; set; }
    }
}
