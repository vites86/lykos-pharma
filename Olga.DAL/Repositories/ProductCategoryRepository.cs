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
    public class ProductCategoryRepository : IRepository<ProductCategory>
    {
        private ProductContext db;

        public ProductCategoryRepository(ProductContext db)
        {
            this.db = db;
        }
        public IQueryable<ProductCategory> GetAll()
        {
            return db.ProductCategories;
        }

        public IQueryable<ProductCategory> GetAll(int countryId)
        {
            return db.ProductCategories;
        }

        public ProductCategory Get(int id)
        {
            return db.ProductCategories.FirstOrDefault(a => a.Id == id);
        }

        public void Create(ProductCategory ProductCategory)
        {
            db.ProductCategories.Add(ProductCategory);
        }

        public void Update(ProductCategory item)
        {
            var existingProductCategory = db.ProductCategories.FirstOrDefault(a => a.Id == item.Id);

            if (existingProductCategory == null) return;

            existingProductCategory.Category = item.Category;
            Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
