using System.Collections.Generic;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ManufacturerDTO
    {
        public ManufacturerDTO()
        {
            this.Products = new List<ProductDTO>();
        }
        public List<ProductDTO> Products { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
