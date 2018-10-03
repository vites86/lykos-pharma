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
        public ProcedureViewModel()
        {
            this.ProcedureDocuments = new List<ProcedureDocument>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ApprovalDate { get; set; }

        public string Comments { get; set; }

        [Required]
        public ProcedureType ProcedureType { get; set; }

        public int? ProductId { get; set; }

        public virtual ProductViewModel Product { get; set; }

        public List<ProcedureDocument> ProcedureDocuments { get; set; }

    }

    public class ProcedureEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ApprovalDate { get; set; }

        public string Comments { get; set; }

        [Required]
        public ProcedureType ProcedureType { get; set; }

        public int? ProductId { get; set; }

        public List<ProcedureDocument> ProcedureDocuments { get; set; }

    }
}