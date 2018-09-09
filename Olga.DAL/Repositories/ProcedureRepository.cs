using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ProcedureRepository : IRepository<Procedure>
    {
        private readonly ProductContext db;

        public ProcedureRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<Procedure> GetAll()
        {
            return db.Procedures;
        }

        IQueryable<Procedure> IRepository<Procedure>.GetAll(int productId)
        {
            return db.Procedures.Where(a=>a.ProductId == productId);
        }

        Procedure IRepository<Procedure>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Procedure item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            Procedure procedure = db.Procedures.Find(id);
            if (procedure != null)
                db.Procedures.Remove(procedure);
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        IQueryable<Procedure> IRepository<Procedure>.GetAll()
        {
            return db.Procedures;
        }

        public IQueryable<Procedure> GetAll(int productId)
        {
            return db.Procedures.Where(a => a.ProductId == productId);
        }

        public Procedure Get(int id)
        {
            return db.Procedures.Find(id);
        }

        public void Create(Procedure procedure)
        {
            db.Procedures.Add(procedure);
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
