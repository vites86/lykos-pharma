using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.DAL.Entities
{
    public class ProcedureDocument
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string PathToDocument { get; set; }

        public ProcedureDocsType ProcedureDocsType { get; set; }

        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; }

        public DateTime? DownloadDt { get; set; }
    }

    public enum ProcedureDocsType
    {
        [Description("Dossier Obtained from MAH")]
        DossierObtainedFromM,

        [Description("Dossier Submitted to Authority")]
        DossierSubmittedToAuth,

        [Description("Remarks obtained from Authority")] 
        RemarksFromAuth,

        [Description("Responses submitted to Authority")] 
        RemarksToAuth
    }
}
