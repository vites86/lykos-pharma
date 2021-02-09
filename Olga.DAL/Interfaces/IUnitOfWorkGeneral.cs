using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities;

namespace Olga.DAL.Interfaces
{
    public interface IUnitOfWorkGeneral : IDisposable
    {
        IProductRepository<Product> Products { get; }
        IProcedureRepository<Procedure> Procedures { get; }
        IRepository<Country> Countries { get; }
        IRepository<ApprDocsType> ApprDocsTypes { get; }
        IRepository<Strength> Strengths { get; }
        IRepository<Manufacturer> Manufacturers { get; }
        IRepository<Artwork> Artworks { get; }
        IRepository<MarketingAuthorizHolder> MarketingAuthorizHolders { get; }
        IRepository<MarketingAuthorizNumber> MarketingAuthorizNumbers { get; }
        IRepository<PharmaceuticalForm> PharmaceuticalForms { get; }
        IRepository<ProductName> ProductNames { get; }
        IRepository<ProductCode> ProductCodes { get; }
        IRepository<CountrySetting> CountrySettings { get; }
        IRepository<ProductStatus> ProductStatuses { get; }
        IRepository<ProductCategory> ProductCategories { get; }
        void Save();
    }
}
