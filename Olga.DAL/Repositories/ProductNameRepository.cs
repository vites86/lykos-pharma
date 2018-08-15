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
    public class ProductNameRepository : IRepository<ProductName>
    {
        private ProductContext db;

        public ProductNameRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<ProductName> GetAll()
        {
            return db.ProductNames;
        }

        public IQueryable<ProductName> GetAll(int countryId)
        {
            return db.ProductNames.Where(a=>a.CountryId== countryId);
        }

        public ProductName Get(int id)
        {
            return db.ProductNames.FirstOrDefault(a=>a.Id == id);
        }

        public void Create(ProductName productName)
        {
            if (!db.ProductNames.Any(e => e.Name == productName.Name && e.CountryId==productName.CountryId))
            {
                db.ProductNames.Add(productName);
            }
        }

        public void Update(ProductName productName)
        {
            if (db.ProductNames.Any(e => e.Name == productName.Name))
                db.Entry(productName).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductName productName = db.ProductNames.Find(id);
            if (productName != null)
                db.ProductNames.Remove(productName);
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
