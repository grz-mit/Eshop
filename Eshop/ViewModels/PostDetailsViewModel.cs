using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class PostDetailsViewModel
    {
        public PostModel Post { get; set; }
        public List<PostModel> OtherPosts { get; set; }
        public bool InCart { get; set; }   
        public string UserId { get; set; }
    }
}
