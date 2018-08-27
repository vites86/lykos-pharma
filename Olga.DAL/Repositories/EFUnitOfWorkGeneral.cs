using System;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class EFUnitOfWorkGeneral : IUnitOfWorkGeneral
    {
        private ProductContext db;
        private ProductRepository productRepository;
        private ProcedureRepository procedurerRepository;

        public EFUnitOfWorkGeneral(string connectionString)
        {
            db = new ProductContext(connectionString);
        }
        public IProductRepository<Product> Products
        {
            get
            {
                if (productRepository == null) productRepository = new ProductRepository(db);
                return productRepository;
            }
        }

        public IRepository<Procedure> Procedures
        {
            get
            {
                if (procedurerRepository == null) procedurerRepository = new ProcedureRepository(db);
                return procedurerRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}