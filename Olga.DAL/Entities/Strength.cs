using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("Strengths", Schema = "info")]
    public class Strength
    {
        public int Id { get; set; }
        public string Strngth { get; set; }
    }
}
