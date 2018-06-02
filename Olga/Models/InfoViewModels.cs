using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Olga.BLL.DTO;
using Olga.DAL.Entities;

namespace Olga.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<ProductNameDTO> ProductNames { get; set; }
        public virtual IEnumerable<ProductCodeDTO> ProductCodes { get; set; }
        public virtual IEnumerable<MarketingAuthorizNumberDTO> MarketingAuthorizNumbers { get; set; }
        public virtual IEnumerable<PackSizeDTO> PackSizes { get; set; }
    }

    public class ProductNameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }

    public class ProductCodeViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? CountryId { get; set; }
    }

    public class MarketingAuthorizNumberViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? CountryId { get; set; }
    }

    public class PackSizeViewModel
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int? CountryId { get; set; }
    }


}