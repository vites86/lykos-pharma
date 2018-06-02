using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class PackSizeDTO
    {
        public int Id { get; set; }
        public string Size { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
