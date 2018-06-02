using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Olga.BLL.DTO;
using Olga.DAL.Entities;

namespace Olga.Models
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        [Required]
        public DateTime ApprovalDate { get; set; }
        public string Comments { get; set; }
        [Required]
        public ProcedureType ProcedureType { get; set; }
        public int? ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public IEnumerable<RemarkViewModel> Remarks { get; set; }
    }

    public class RemarkViewModel
    {
        public int Id { get; set; }
        public DateTime RemarkDate { get; set; }
        public string Comments { get; set; }
        public ProcedureViewModel Procedure { get; set; }
    }
}