using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class ImageModel
    {
        public int Id { get; set; }

        public int GalleryId { get; set; }

        public string Name { get; set; }

        public GalleryModel Gallery { get; set; }
    }
}
