using System.Collections.Generic;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public sealed class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductNameDTO> ProductNames { get; set; }
        public ICollection<ProductCodeDTO> ProductCodes { get; set; }
        public ICollection<MarketingAuthorizNumberDTO> MarketingAuthorizNumbers { get; set; }
        public ICollection<PackSizeDTO> PackSizes { get; set; }

        public CountryDTO()
        {
            ProductNames = new List<ProductNameDTO>();
            ProductCodes = new List<ProductCodeDTO>();
            MarketingAuthorizNumbers = new List<MarketingAuthorizNumberDTO>();
            PackSizes = new List<PackSizeDTO>();
        }
        public CountrySettingDTO CountrySettings { get; set; }

    }
}
