using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Olga.DAL.Entities.Account;

namespace Olga.DAL.Entities
{
    [Table("Countries", Schema = "info")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductName> ProductNames { get; set; }
        public virtual ICollection<ProductCode> ProductCodes { get; set; }
        public virtual ICollection<MarketingAuthorizNumber> MarketingAuthorizNumbers { get; set; }
        public virtual ICollection<PackSize> PackSizes { get; set; }
        public virtual ICollection<ClientProfile> Users { get; set; }

        public Country()
        {
            ProductNames = new List<ProductName>();
            MarketingAuthorizNumbers = new List<MarketingAuthorizNumber>();
            PackSizes = new List<PackSize>();
            ProductCodes = new List<ProductCode>();
            Users = new List<ClientProfile>();
            //Products = new List<Product>();
        }
        public virtual CountrySetting CountrySettings { get; set; }

        //public virtual IEnumerable<Product> Products { get; set; }
    }
}
