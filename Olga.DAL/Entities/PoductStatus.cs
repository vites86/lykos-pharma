using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("ProductStatuses", Schema = "info")]
    public class ProductStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
