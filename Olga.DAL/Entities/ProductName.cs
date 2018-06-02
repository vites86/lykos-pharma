using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("ProductNames", Schema = "info")]
    public class ProductName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
