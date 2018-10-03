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
    public class PharmaceuticalFormRepository : IRepository<PharmaceuticalForm>
    {
        private ProductContext db;

        public PharmaceuticalFormRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<PharmaceuticalForm> GetAll()
        {
            return db.PharmaceuticalForms;
        }

        public IQueryable<PharmaceuticalForm> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public PharmaceuticalForm Get(int id)
        {
            return db.PharmaceuticalForms.Find(id);
        }

        public void Create(PharmaceuticalForm pharmaceuticalForm)
        {
            if (!db.PharmaceuticalForms.Any(e => e.PharmaForm == pharmaceuticalForm.PharmaForm))
            {
                db.PharmaceuticalForms.Add(pharmaceuticalForm);
            }
        }

        public void Update(PharmaceuticalForm pharmaceuticalForm)
        {
            db.Entry(pharmaceuticalForm).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PharmaceuticalForm pharmaceuticalForm = db.PharmaceuticalForms.Find(id);
            if (pharmaceuticalForm == null) return;
            var products = db.Products.Where(a => a.PharmaceuticalFormId == id);
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.PharmaceuticalForm = null;
                }
            }
            db.PharmaceuticalForms.Remove(pharmaceuticalForm);
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
