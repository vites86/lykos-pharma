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
    public class ApprDocsTypeRepository : IRepository<ApprDocsType>
    {
        private ProductContext db;

        public ApprDocsTypeRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<ApprDocsType> GetAll()
        {
            return db.ApprDocsTypes;
        }

        public IQueryable<ApprDocsType> GetAll(int countryId)
        {
            throw new NotImplementedException();
        }

        public ApprDocsType Get(int id)
        {
            return db.ApprDocsTypes.FirstOrDefault(p => p.Id == id);
        }

        public void Create(ApprDocsType apprDocsType)
        {
            if (!db.ApprDocsTypes.Any(e => e.ApprType == apprDocsType.ApprType))
            {
                db.ApprDocsTypes.Add(apprDocsType);
            }
        }

        public void Update(ApprDocsType apprDocsType)
        {
            db.Entry(apprDocsType).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ApprDocsType apprDocsType = db.ApprDocsTypes.Find(id);
            if (apprDocsType != null)
                db.ApprDocsTypes.Remove(apprDocsType);
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
