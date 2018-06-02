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

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime SubmissionDate { get; set; }
        public DateTime ApprovalDate { get; set; }

        public string Comments { get; set; }

        public ProcedureType ProcedureType { get; set; }

        public IEnumerable<RemarkDTO> Remarks { get; set; }

        public int? ProductId { get; set; }
        public ProductDTO Product { get; set; }

    }

    public class RemarkDTO
    {
        public int Id { get; set; }
        public DateTime RemarkDate { get; set; }
        public string Comments { get; set; }
        public ProcedureDTO Procedure { get; set; }
    }
}
