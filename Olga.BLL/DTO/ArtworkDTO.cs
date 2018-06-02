using System.Collections.Generic;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ArtworkDTO
    {
        public ArtworkDTO()
        {
            this.Products = new List<ProductDTO>();
        }
        public List<ProductDTO> Products { get; set; }

        public int Id { get; set; }
        public string Artwork_name { get; set; }
    }
}
