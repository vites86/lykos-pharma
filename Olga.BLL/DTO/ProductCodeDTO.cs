using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ProductCodeDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public virtual Country Country { get; set; }
        public int? CountryId { get; set; }
    }
}
