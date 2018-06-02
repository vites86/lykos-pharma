using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace Olga.DAL.Entities
{
    [Table("ApprDocsTypes", Schema = "info")]
    public class ApprDocsType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ApprType { get; set; }

        public ApprDocsType()
        {
            this.Products = new HashSet<Product>();
        }
        public virtual ICollection<Product> Products { get; set; }

        
    }
}
