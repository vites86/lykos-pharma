using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ProductStatusRepository : IRepository<ProductStatus>
    {
        private ProductContext db;

        public ProductStatusRepository(ProductContext db)
        {
            this.db = db;
        }
        public IQueryable<ProductStatus> GetAll()
        {
            return db.ProductStatuses;
        }

        public IQueryable<ProductStatus> GetAll(int countryId)
        {
            return db.ProductStatuses;
        }

        public ProductStatus Get(int id)
        {
            return db.ProductStatuses.FirstOrDefault(a => a.Id == id);
        }

        public void Create(ProductStatus productStatus)
        {
            db.ProductStatuses.Add(productStatus);
        }

        public void Update(ProductStatus item)
        {
            var existingProductStatus = db.ProductStatuses.FirstOrDefault(a => a.Id == item.Id);

            if (existingProductStatus == null) return;

            existingProductStatus.Status = item.Status;
            Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Commit()
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
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
