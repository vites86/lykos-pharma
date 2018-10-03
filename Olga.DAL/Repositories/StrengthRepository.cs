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
    public class StrengthRepository : IRepository<Strength>
    {
        private ProductContext db;

        public StrengthRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<Strength> GetAll()
        {
            return db.Strengths;
        }

        public IQueryable<Strength> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public Strength Get(int id)
        {
            return db.Strengths.Find(id);
        }

        public void Create(Strength strength)
        {
            if (!db.Strengths.Any(e => e.Strngth == strength.Strngth))
            {
                db.Strengths.Add(strength);
            }
        }

        public void Update(Strength strength)
        {
            db.Entry(strength).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Strength strength = db.Strengths.Find(id);
            if (strength == null) return;
            var products = db.Products.Where(a => a.StrengthId == id);
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.Strength = null;
                }
            }
            db.Strengths.Remove(strength);
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
