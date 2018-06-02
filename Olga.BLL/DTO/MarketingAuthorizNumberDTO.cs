using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class MarketingAuthorizNumberDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
