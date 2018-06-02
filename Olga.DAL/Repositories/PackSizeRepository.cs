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
    public class PackSizeRepository : IRepository<PackSize>
    {
        private ProductContext db;

        public PackSizeRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<PackSize> GetAll()
        {
            return db.PackSizes;
        }

        public IQueryable<PackSize> GetAll(int countryId)
        {
            return db.PackSizes.Where(a => a.CountryId == countryId);
        }

        public PackSize Get(int id)
        {
            return db.PackSizes.FirstOrDefault(a => a.Id == id);
        }

        public void Create(PackSize packSize)
        {
            if (!db.PackSizes.Any(e => e.Size == packSize.Size))
            {
                db.PackSizes.Add(packSize);
            }
        }

        public void Update(PackSize packSize)
        {
            db.Entry(packSize).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PackSize packSize = db.PackSizes.Find(id);
            if (packSize != null)
                db.PackSizes.Remove(packSize);
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
