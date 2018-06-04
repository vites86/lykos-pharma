using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olga.DAL.Entities;

namespace Olga.Models
{
    public class ArtworkViewModel
    {
        public ArtworkViewModel()
        {
            this.ProductDocuments =  new List<ProductDocument>();
            this.Products = new List<ProductViewModel>();
        }
        public List<ProductDocument> ProductDocuments { get; set; }
        public List<ProductViewModel> Products { get; set; }

        public int Id { get; set; }
        public string Artwork_name { get; set; }
    }
}