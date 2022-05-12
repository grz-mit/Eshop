using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class ReceivedMessageModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReplyId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public string SenderName { get; set; }
        public string OfferTitle { get; set; }
        public DateTime SentDate { get; set; }
        public AppUser User { get; set; }
        [NotMapped]
        public SentMessageModel SentMessageModel { get; set; }
    }
}
