using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.DTO
{
    public class MessageDTO
    {
        public string UserId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public string ReceiverName { get; set; }
        public string OfferTitle { get; set; }
        public string OfferOwnerId { get; set; }
    }
}
