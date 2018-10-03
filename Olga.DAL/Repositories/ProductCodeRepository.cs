using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ProductCodeRepository : IRepository<ProductCode>
    {
        private ProductContext db;

        public ProductCodeRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<ProductCode> GetAll()
        {
            return db.ProductCodes;
        }

        public IQueryable<ProductCode> GetAll(int countryId)
        {
            return db.ProductCodes.Where(a => a.CountryId == countryId);
        }

        public ProductCode Get(int id)
        {
            return db.ProductCodes.FirstOrDefault(a => a.Id == id);
        }

        public void Create(ProductCode productCode)
        {
            if (!db.ProductCodes.Any(e => e.Code == productCode.Code && e.CountryId == productCode.CountryId))
            {
                db.ProductCodes.Add(productCode);
            }
        }

        public void Update(ProductCode productCode)
        {
            db.Entry(productCode).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductCode productCode = db.ProductCodes.Find(id);
            if (productCode == null) return;
            var products = db.Products.Where(a => a.ProductCodeId == id);
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.ProductCode = null;
                }
            }
            db.ProductCodes.Remove(productCode);
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
