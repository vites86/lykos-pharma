using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Owin.Security.Provider;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;
using Olga.DAL.Repositories;

namespace Olga.BLL.Services
{
    public class ProductService : IProductService
    {
        //private ProductRepository Database { get; set; }

        //public ProductService(ProductContext context)
        //{
        //   Database = new ProductRepository(context);
        //}

        IUnitOfWorkGeneral Database { get; set; }
        public ProductService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public  void AddProduct(ProductDTO productDto, string[] selectedManufacturers, string[] selectedArtworks)
        {
            Product product = Mapper.Map<ProductDTO, Product>(productDto);
            //Database.Create(product, selectedManufacturers, selectedArtworks);
            Database.Products.Create(product, selectedManufacturers, selectedArtworks);
        }

        public ProductDTO GetProduct(int id)
        {
            //return Mapper.Map<Product,ProductDTO > (Database.Get(id));
            return Mapper.Map<Product,ProductDTO > (Database.Products.Get(id));
        }

        public IEnumerable<ProductDTO> GetProducts(int? countryId)
        {
            if (countryId == null)
            {
               return null;
            }

            //var productList = Database.GetAll((int)countryId).ToList();
            var productList = Database.Products.GetAll((int)countryId).ToList();
            return productList.Any() ? Mapper.Map<List<Product>, List<ProductDTO>>(productList.ToList()) : new List<ProductDTO>();
        }

        public ApprDocsTypeDTO GetApprDocsType(int id)
        {
            //return Mapper.Map<ApprDocsType, ApprDocsTypeDTO>(Database.GetApprDocsType(id));
            return Mapper.Map<ApprDocsType, ApprDocsTypeDTO>(Database.Products.GetApprDocsType(id));
        }

        public ArtworkDTO GetArtwork(int id)
        {
            //return Mapper.Map<Artwork, ArtworkDTO>(Database.GetArtwork(id));
            return Mapper.Map<Artwork, ArtworkDTO>(Database.Products.GetArtwork(id));
        }

        public ManufacturerDTO GetManufacturer(int id)
        {
            //return Mapper.Map<Manufacturer, ManufacturerDTO>(Database.GetManufacturer(id));
            return Mapper.Map<Manufacturer, ManufacturerDTO>(Database.Products.GetManufacturer(id)); 
        }

        public void DeleteProduct(int id)
        {
            //Database.Delete(id);
            Database.Products.Delete(id);
        }

        public void Dispose()
        {
            //Database.Dispose();
            Database.Dispose();
        }
        public void Commit()
        {
            //Database.Commit();
            Database.Products.Commit();
        }

        public void DeleteDocument(string name)
        {
            if(string.IsNullOrEmpty(name)) return;
            //Database.DeleteDocuments(name);
            Database.Products.DeleteDocuments(name);
        }
    }
}
