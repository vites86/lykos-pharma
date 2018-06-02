using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olga.Models
{
    public class ApprDocsTypeViewModel
    {
        public ApprDocsTypeViewModel()
        {
            this.Products = new List<ProductViewModel>();
        }
        public List<ProductViewModel> Products { get; set; }

        public int Id { get; set; }
        public string ApprType { get; set; }
    }
}