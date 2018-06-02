using System.Collections.Generic;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ApprDocsTypeDTO
    {
        public ApprDocsTypeDTO()
        {
            this.Products = new List<ProductDTO>();
        }
        public List<ProductDTO> Products { get; set; }

        public int Id { get; set; }
        public string ApprType { get; set; }
    }
}
