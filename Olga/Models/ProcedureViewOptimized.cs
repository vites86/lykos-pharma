using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olga.Models
{
    public class ProcedureViewOptimized
    {       

        public int Id { get; set; }
        
        public string EstimatedSubmissionDate { get; set; }

        public string Name { get; set; }
       
        public string SubmissionDate { get; set; }

        public string EstimatedApprovalDate { get; set; }

        public string ApprovalDate { get; set; }

        public string Comments { get; set; }

        public string ProcedureType { get; set; }

        public int? ProductId { get; set; }

        public string ProductInfo { get; set; }
    }
}