using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("ProductCodes", Schema = "info")]
    public class ProductCode
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
