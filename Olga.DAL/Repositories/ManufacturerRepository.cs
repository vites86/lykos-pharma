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
    public class ManufacturerRepository : IRepository<Manufacturer>
    {
        private ProductContext db;

        public ManufacturerRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<Manufacturer> GetAll()
        {
            return db.Manufacturers;
        }

        public IQueryable<Manufacturer> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public Manufacturer Get(int id)
        {
            return db.Manufacturers.Find(id);
        }

        public void Create(Manufacturer manufacturer)
        {
            if (!db.Manufacturers.Any(e => e.Name == manufacturer.Name))
            {
                db.Manufacturers.Add(manufacturer);
            }
        }

        public void Update(Manufacturer manufacturer)
        {
            db.Entry(manufacturer).State = EntityState.Modified;
        }

        public void Delete(int id)
        {

            Manufacturer manufacturer = db.Manufacturers.Find(id);

            if (manufacturer == null) return;
            var products = db.Products.Where(a=>a.Manufacturers.Contains(manufacturer));
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.Manufacturers.Remove(manufacturer);
                }
            }
            db.Manufacturers.Remove(manufacturer);
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
