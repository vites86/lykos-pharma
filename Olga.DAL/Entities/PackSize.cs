using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("PackSizes", Schema = "info")]
    public class PackSize
    {
        public int Id { get; set; }
        public string Size { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
