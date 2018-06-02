using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("MarketingAuthorizNumbers", Schema = "info")]
    public class MarketingAuthorizNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
