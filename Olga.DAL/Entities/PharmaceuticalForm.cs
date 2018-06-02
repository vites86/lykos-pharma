using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("PharmaceuticalForms", Schema = "info")]
    public class PharmaceuticalForm
    {
        public int Id { get; set; }
        public string PharmaForm { get; set; }
    }
}
