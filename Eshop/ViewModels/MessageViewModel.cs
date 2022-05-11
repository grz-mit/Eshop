using Eshop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class MessageViewModel
    {
        public MessageDTO MessageDTO { get; set; }
        public string UserName { get; set; }       
        public string OfferTitle { get; set; }
    }
}
