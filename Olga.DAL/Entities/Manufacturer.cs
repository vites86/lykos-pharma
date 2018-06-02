using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("Manufacturers", Schema = "info")]
    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Product>();
        }
        public virtual ICollection<Product> Products { get; set; }
        public int ProductId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        
    }
}
