using System.Collections.Generic;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ApprDocsTypeDTO
    {
        public ApprDocsTypeDTO()
        {
            this.ProductDocuments = new List<ProductDocument>();
        }
        public List<ProductDTO> Products { get; set; }

        public int Id { get; set; }
        public string ApprType { get; set; }

        public List<ProductDocument> ProductDocuments { get; set; }

    }
}
