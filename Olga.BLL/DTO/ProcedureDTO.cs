using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities;

namespace  Olga.BLL.DTO
{
    public class ProcedureDTO
    {
        public ProcedureDTO()
        {
            this.ProcedureDocuments = new List<ProcedureDocument>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime EstimatedApprovalDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Comments { get; set; }
        public ProcedureType ProcedureType { get; set; }

        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }

        public List<ProcedureDocument> ProcedureDocuments { get; set; }
    }

    
}
