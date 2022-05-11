using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class ReplyComModel
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string Com { get; set; }
        public string NickName { get; set; }
        public CommentModel Comment { get; set; }
    }
}
