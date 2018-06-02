using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("MarketingAuthorizHolders", Schema = "info")]
    public class MarketingAuthorizHolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
