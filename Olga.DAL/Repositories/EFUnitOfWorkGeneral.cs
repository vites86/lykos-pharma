using System;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class EfUnitOfWorkGeneral : IUnitOfWorkGeneral
    {
        private readonly ProductContext _db;
        private ProductRepository _productRepository;
        private ProcedureRepository _procedurerRepository;
        private CountryRepository _countryRepository;
        private StrengthRepository _strengthRepository;
        private ManufacturerRepository _manufacturerRepository;
        private ArtworkRepository _artworkRepository;
        private ApprDocsTypeRepository _apprDocsTypeRepository;
        private MarketingAuthorizNumberRepository _marketingAuthorizNumberRepository;
        private MarketingAuthorizHolderRepository _marketingAuthorizHolderRepository;
        private PharmaceuticalFormRepository _pharmaceuticalFormRepository;
        private ProductNameRepository _productNameRepository;
        private ProductCodeRepository _productCodeRepository;

        public EfUnitOfWorkGeneral(string connectionString)
        {
            _db = new ProductContext(connectionString);
        }
        public IProductRepository<Product> Products => _productRepository ?? (_productRepository = new ProductRepository(_db));
        public IRepository<Procedure> Procedures => _procedurerRepository ?? (_procedurerRepository = new ProcedureRepository(_db));
        public IRepository<Country> Countries => _countryRepository ?? (_countryRepository = new CountryRepository(_db));
        public IRepository<ApprDocsType> ApprDocsTypes => _apprDocsTypeRepository ?? (_apprDocsTypeRepository = new ApprDocsTypeRepository(_db));
        public IRepository<Strength> Strengths => _strengthRepository ?? (_strengthRepository = new StrengthRepository(_db));
        public IRepository<Manufacturer> Manufacturers => _manufacturerRepository ?? (_manufacturerRepository = new ManufacturerRepository(_db));
        public IRepository<Artwork> Artworks => _artworkRepository ?? (_artworkRepository = new ArtworkRepository(_db));
        public IRepository<MarketingAuthorizNumber> MarketingAuthorizNumbers => _marketingAuthorizNumberRepository ?? (_marketingAuthorizNumberRepository = new MarketingAuthorizNumberRepository(_db));
        public IRepository<MarketingAuthorizHolder> MarketingAuthorizHolders => _marketingAuthorizHolderRepository ?? (_marketingAuthorizHolderRepository = new MarketingAuthorizHolderRepository(_db));
        public IRepository<PharmaceuticalForm> PharmaceuticalForms => _pharmaceuticalFormRepository ?? (_pharmaceuticalFormRepository = new PharmaceuticalFormRepository(_db));
        public IRepository<ProductName> ProductNames => _productNameRepository ?? (_productNameRepository = new ProductNameRepository(_db));
        public IRepository<ProductCode> ProductCodes => _productCodeRepository ?? (_productCodeRepository = new ProductCodeRepository(_db));

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}