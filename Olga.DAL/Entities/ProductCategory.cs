using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("ProductCategory", Schema = "info")]
    [DefaultValue("Medicine")]
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
    }
}
