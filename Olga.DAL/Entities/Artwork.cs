
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("Artworks", Schema = "info")]
    public class Artwork
    {
        public Artwork()
        {
            this.Products = new HashSet<Product>();
            this.ProductDocuments =  new HashSet<ProductDocument>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Artwork_name { get; set; }

        public virtual ICollection<ProductDocument> ProductDocuments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
