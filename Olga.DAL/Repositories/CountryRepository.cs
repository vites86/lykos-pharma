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
    public class CountryRepository : IRepository<Country>
    {
        private ProductContext db;

        public CountryRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<Country> GetAll()
        {
            return db.Countries;
        }

        public IQueryable<Country> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public Country Get(int id)
        {
            return db.Countries.FirstOrDefault(a=>a.Id == id);
        }

        public void Create(Country country)
        {
            db.Countries.Add(country);
        }

        public void Update(Country country)
        {
            db.Entry(country).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Country country = db.Countries.Find(id);
            if (country != null)
                db.Countries.Remove(country);
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
