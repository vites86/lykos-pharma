using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ProcedureRepository : IProcedureRepository<Procedure>
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
            return db.Procedures.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Procedure item)
        {
            var existingProcedure = db.Procedures.FirstOrDefault(a => a.Id == item.Id);
            if (existingProcedure == null) return;
            existingProcedure.Name = item.Name;
            existingProcedure.ApprovalDate = item.ApprovalDate;
            existingProcedure.EstimatedApprovalDate = item.EstimatedApprovalDate;
            existingProcedure.Comments = item.Comments;
            existingProcedure.ProcedureType = item.ProcedureType;
            existingProcedure.SubmissionDate = item.SubmissionDate;
            existingProcedure.EstimatedSubmissionDate = item.EstimatedSubmissionDate;

            foreach (var document in item.ProcedureDocuments)
            {
                var res = existingProcedure.ProcedureDocuments.FirstOrDefault(a => a.PathToDocument.Equals(document.PathToDocument));
                if (res != null) continue;
                document.DownloadDt = DateTime.Now;
                existingProcedure.ProcedureDocuments.Add(document);
            }

            //var listOfDocs = existingProcedure.ProcedureDocuments.ToList();
            //foreach (var doc in listOfDocs)
            //{
            //    var res = item.ProcedureDocuments.FirstOrDefault(a => a.PathToDocument.Equals(doc.PathToDocument));
            //    if (res != null) continue;
            //    existingProcedure.ProcedureDocuments.Remove(doc);
            //    DeleteDocument(doc.Id);
            //}
            Commit();
        }
        
        public void UpdateDocument(Procedure item)
        {
            var existingProcedure = db.Procedures.FirstOrDefault(a => a.Id == item.Id);
            if (existingProcedure == null) return;
            foreach (var document in item.ProcedureDocuments)
            {
                var res = existingProcedure.ProcedureDocuments.FirstOrDefault(a => a.PathToDocument.Equals(document.PathToDocument));
                if(res==null) continue;
                existingProcedure.ProcedureDocuments.Add(document);
                Commit();
            }
        }

        public void DeleteDocument(int id)
        {
            var doc = db.ProcedureDocuments.FirstOrDefault(a => a.Id == id);
            if (doc != null)
            {
                db.ProcedureDocuments.Remove(doc);
            }
            Commit();
        }

        public void DeleteDocument(string fileName)
        {
            var doc = db.ProcedureDocuments.FirstOrDefault(p => p.PathToDocument.Equals(fileName));
            if (doc != null)
            {
                db.ProcedureDocuments.Remove(doc);
                Commit();
            }
        }

        public void Delete(int id)
        {
            Procedure procedure = db.Procedures.Find(id);
            if (procedure != null)
            {
                var documents = procedure.ProcedureDocuments.ToList();
                foreach (var doc in documents)
                {
                    DeleteDocument(doc.Id);
                }
                db.Procedures.Remove(procedure);
            }
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
