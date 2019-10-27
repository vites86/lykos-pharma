using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Olga.BLL.DTO;
using Olga.DAL.Entities;
using Resources;

namespace Olga.Models
{
    public class ProcedureViewModel
    {

        public ProcedureViewModel()
        {
            this.ProcedureDocuments = new List<ProcedureDocument>();
        }

        public int Id { get; set; }

        [Display(Name = "EstimatedSubmissionDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstimatedSubmissionDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "SubmissionDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        [Required]
        [Display(Name = "EstimatedApprovalDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EstimatedApprovalDate { get; set; }

        [Display(Name = "ApprovalDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalDate { get; set; }

        [Required]
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

        [Required]
        public string Name { get; set; }

        [Display(Name = "EstimatedSubmissionDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstimatedSubmissionDate { get; set; }

        [Required]
        [Display(Name = "SubmissionDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        [Required]
        [Display(Name = "EstimatedApprovalDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime EstimatedApprovalDate { get; set; }

        [Display(Name = "ApprovalDate", ResourceType = typeof(Resources.Labels))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalDate { get; set; }

        public string Comments { get; set; }

        [Required]
        public ProcedureType ProcedureType { get; set; }

        public int? ProductId { get; set; }

        public List<ProcedureDocument> ProcedureDocuments { get; set; }

    }
}