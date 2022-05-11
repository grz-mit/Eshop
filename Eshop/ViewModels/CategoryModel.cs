using Eshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class CategoryModel
    {
        public List<PostModel> Posts { get; set; }
        public SelectList Genres { get; set; }
    }
}
