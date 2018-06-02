using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.DAL.Entities
{
    public class Procedure
    {
        public Procedure()
        {
            this.Remarks = new HashSet<Remark>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string Comments { get; set; }
        public ProcedureType ProcedureType { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Remark> Remarks { get; set; }
    }

    public class Remark
    {
        public int Id { get; set; }
        public DateTime RemarkDate { get; set; }
        public string Comments { get; set; }
        public virtual Procedure Procedure { get; set; }
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
