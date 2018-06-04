using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace Olga.DAL.Entities
{
    [Table("ApprDocsTypes", Schema = "info")]
    public class ApprDocsType
    {
        public ApprDocsType()
        {
            this.ProductDocuments = new HashSet<ProductDocument>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ApprType { get; set; }
       
        public virtual ICollection<ProductDocument> ProductDocuments { get; set; }
    }
}
