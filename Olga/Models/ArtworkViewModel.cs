using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olga.Models
{
    public class ArtworkViewModel
    {
        public ArtworkViewModel()
        {
            this.Products = new List<ProductViewModel>();
        }
        public List<ProductViewModel> Products { get; set; }

        public int Id { get; set; }
        public string Artwork_name { get; set; }
    }
}