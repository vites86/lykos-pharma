using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olga.DAL.Entities;

namespace Olga.Models
{
    public class ApprDocsTypeViewModel
    {
        public ApprDocsTypeViewModel()
        {
            this.ProductDocuments = new List<ProductDocument>();
        }
        public List<ProductViewModel> Products { get; set; }

        public int Id { get; set; }
        public string ApprType { get; set; }

        public List<ProductDocument> ProductDocuments { get; set; }
    }
}