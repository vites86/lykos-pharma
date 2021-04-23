using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly ProductContext db;

        public ProductRepository(ProductContext context)
        {
            this.db = context;
        }

        public IQueryable<Product> GetAll()
        {
           return db.Products;
        }

        public IQueryable<Product> GetAll(int countryId)
        {
            return db.Products.Where(a=>a.CountryId==countryId);
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public void Create(Product product)
        {
            db.Products.Add(product);
        }

        public void Create(Product product, string[] selectedManufacturers, string[] selectedArtworks)
        {
            db.Products.AddOrUpdate(product);

            if (product.Id != 0)
            {
                Update(product, selectedManufacturers, selectedArtworks);
                return;
            }

            if (selectedManufacturers != null)
            {
                foreach (var id in selectedManufacturers)
                {
                    if (string.IsNullOrEmpty(id)) continue;
                    var manufacturer = db.Manufacturers.Find(int.Parse(id));
                    product.Manufacturers.Add(manufacturer);
                }
            }

            if (selectedArtworks != null)
            {
                foreach (var id in selectedArtworks)
                {
                    if (string.IsNullOrEmpty(id)) continue;
                    var artwork = db.Artworks.Find(int.Parse(id));
                    product.Artworks.Add(artwork);
                }
            }
        }

        public void Update(Product product)
        {
        }

        public void Update(Product product, string[] selectedManufacturers, string[] selectedArtworks)
        {
            var existingProduct = db.Products.FirstOrDefault(a=>a.Id == product.Id);

            if (existingProduct == null) return;

            existingProduct.Artworks.Clear();
            existingProduct.Manufacturers.Clear();


            foreach (var doc in product.ProductDocuments)
            {
                var appr = db.ProductDocuments.Any(a=>a.PathToDocument == doc.PathToDocument);
                if (!appr)
                {
                    existingProduct.ProductDocuments.Add(new ProductDocument(){PathToDocument = doc.PathToDocument, ProductId = product.Id, ApprDocsTypeId = doc.ApprDocsTypeId, ArtworkId = doc.ArtworkId });
                }
            }

            if (selectedManufacturers != null)
            {
                foreach (var newManuf in selectedManufacturers)
                {
                    var manuf = db.Manufacturers.Find(int.Parse(newManuf));
                    existingProduct.Manufacturers.Add(manuf);
                }
            }

            if (selectedArtworks != null)
            {
                foreach (var newArtw in selectedArtworks)
                {
                    var artw = db.Artworks.Find(int.Parse(newArtw));
                    existingProduct.Artworks.Add(artw);
                }
            }
        }

        public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
                db.Products.Remove(product);
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

        public ApprDocsType GetApprDocsType(int id)
        {
            return db.ApprDocsTypes.FirstOrDefault(p => p.Id == id);
        }

        public Artwork GetArtwork(int id)
        {
            return db.Artworks.FirstOrDefault(p => p.Id == id); ;
        }

        public Manufacturer GetManufacturer(int id)
        {
            return db.Manufacturers.FirstOrDefault(p => p.Id == id); ;
        }

        public void DeleteDocuments(string fileName)
        {
            var doc = db.ProductDocuments.FirstOrDefault(p => p.PathToDocument.Equals(fileName));
            if (doc!=null)
            {
                db.ProductDocuments.Remove(doc);
                Commit();
            }
        }
        public async Task<Product> FindAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }


    }
}
