using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.DTO
{
    public class CreateCommentDTO
    {
        public string UserId { get; set; }
        public string ReceiverUserId { get; set; }
        public int OfferEndedId { get; set; }
        public string Rate { get; set; }
        public string Com { get; set; }
    }
}
