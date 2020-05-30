using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.DAL.Entities
{
    public class Procedure
    {
        public Procedure()
        {
            this.ProcedureDocuments = new HashSet<ProcedureDocument>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime EstimatedApprovalDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalDate { get; set; } = null;

        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstimatedSubmissionDate { get; set; }

        public string Comments { get; set; }
        public ProcedureType ProcedureType { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<ProcedureDocument> ProcedureDocuments { get; set; }
    }

    public enum ProcedureType
    {
        NewRegistration,
        Renewal,
        Variations,
        GMPconfirmation,
        GMPinspection
    }


}
