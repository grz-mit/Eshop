using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class SentMessageModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public string ReceiverName { get; set; }
        public string PostName { get; set; }
        public DateTime SentDate { get; set; }
        public AppUser User { get; set; }
    }
}
