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
    public class ArtworkRepository : IRepository<Artwork>
    {
        private ProductContext db;

        public ArtworkRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<Artwork> GetAll()
        {
            return db.Artworks;
        }

        public IQueryable<Artwork> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public Artwork Get(int id)
        {
            return db.Artworks.Find(id);
        }

        public void Create(Artwork artwork)
        {
            if (!db.Artworks.Any(e => e.Artwork_name == artwork.Artwork_name))
            {
                db.Artworks.Add(artwork);
            }
        }

        public void Update(Artwork artwork)
        {
            db.Entry(artwork).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            if (artwork != null)
                db.Artworks.Remove(artwork);
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
