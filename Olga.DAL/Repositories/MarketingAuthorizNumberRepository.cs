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
    public class MarketingAuthorizNumberRepository : IRepository<MarketingAuthorizNumber>
    {
        private ProductContext db;

        public MarketingAuthorizNumberRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<MarketingAuthorizNumber> GetAll()
        {
            return db.MarketingAuthorizNumbers;
        }

        public IQueryable<MarketingAuthorizNumber> GetAll(int countryId)
        {
            return db.MarketingAuthorizNumbers.Where(a => a.CountryId == countryId);
        }

        public MarketingAuthorizNumber Get(int id)
        {
            return db.MarketingAuthorizNumbers.FirstOrDefault(a => a.Id == id);
        }

        public void Create(MarketingAuthorizNumber marketingAuthorizNumber)
        {
            if (!db.MarketingAuthorizNumbers.Any(e => e.Number == marketingAuthorizNumber.Number && e.CountryId == marketingAuthorizNumber.CountryId))
            {
                db.MarketingAuthorizNumbers.Add(marketingAuthorizNumber);
            }
        }

        public void Update(MarketingAuthorizNumber marketingAuthorizNumber)
        {
            db.Entry(marketingAuthorizNumber).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            MarketingAuthorizNumber marketingAuthorizNumber = db.MarketingAuthorizNumbers.Find(id);
            if (marketingAuthorizNumber == null) return;
            var products = db.Products.Where(a => a.MarketingAuthorizNumberId == id);
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.MarketingAuthorizNumber = null;
                }
            }
            db.MarketingAuthorizNumbers.Remove(marketingAuthorizNumber);

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
