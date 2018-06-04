using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ProductDocumentDTO
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string PathToDocument { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? ApprDocsTypeId { get; set; }
        public ApprDocsType ApprDocsType { get; set; }

        public int? ArtworkId { get; set; }
        public Artwork Artwork { get; set; }
    }
}
