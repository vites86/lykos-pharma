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
    public class MarketingAuthorizHolderRepository : IRepository<MarketingAuthorizHolder>
    {
        private ProductContext db;

        public MarketingAuthorizHolderRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<MarketingAuthorizHolder> GetAll()
        {
            return db.MarketingAuthorizHolders;
        }

        public IQueryable<MarketingAuthorizHolder> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public MarketingAuthorizHolder Get(int id)
        {
            return db.MarketingAuthorizHolders.Find(id);
        }

        public void Create(MarketingAuthorizHolder marketingAuthorizHolder)
        {
            if (!db.MarketingAuthorizHolders.Any(e => e.Name == marketingAuthorizHolder.Name))
            {
                db.MarketingAuthorizHolders.Add(marketingAuthorizHolder);
            }
        }

        public void Update(MarketingAuthorizHolder marketingAuthorizHolder)
        {
            db.Entry(marketingAuthorizHolder).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            MarketingAuthorizHolder marketingAuthorizHolder = db.MarketingAuthorizHolders.Find(id);
            if (marketingAuthorizHolder == null) return;
            var products = db.Products.Where(a => a.MarketingAuthorizHolderId == id);
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.MarketingAuthorizHolderId = null;
                }
            }
            db.MarketingAuthorizHolders.Remove(marketingAuthorizHolder);
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
