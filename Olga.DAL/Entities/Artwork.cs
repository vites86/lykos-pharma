
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("Artworks", Schema = "info")]
    public class Artwork
    {
        public Artwork()
        {
            this.Products = new HashSet<Product>();
        }
        public virtual ICollection<Product> Products { get; set; }
        public int ProductId { get; set; }

        public int Id { get; set; }
        public string Artwork_name { get; set; }
    }
}
