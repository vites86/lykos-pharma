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
        IUnitOfWorkGeneral Database { get; set; }

        public ProductService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddProduct(ProductDTO productDto, string[] selectedManufacturers, string[] selectedArtworks)
        {
            Product product = Mapper.Map<ProductDTO, Product>(productDto);
            Database.Products.Create(product, selectedManufacturers, selectedArtworks);
        }

        public ProductDTO GetProduct(int id)
        {
            return Mapper.Map<Product,ProductDTO > (Database.Products.Get(id));
        }

        public IEnumerable<ProductDTO> GetProducts(int? countryId)
        {
            if (countryId == null)
            {
               return null;
            }

            var productList = Database.Products.GetAll((int)countryId).ToList();
            return productList.Any() ? Mapper.Map<List<Product>, List<ProductDTO>>(productList.ToList()) : new List<ProductDTO>();
        }

        public ApprDocsTypeDTO GetApprDocsType(int id)
        {
            return Mapper.Map<ApprDocsType, ApprDocsTypeDTO>(Database.Products.GetApprDocsType(id));
        }

        public ArtworkDTO GetArtwork(int id)
        {
            return Mapper.Map<Artwork, ArtworkDTO>(Database.Products.GetArtwork(id));
        }

        public ManufacturerDTO GetManufacturer(int id)
        {
            return Mapper.Map<Manufacturer, ManufacturerDTO>(Database.Products.GetManufacturer(id)); 
        }

        public void DeleteProduct(int id)
        {
            Database.Products.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.Products.Commit();
        }

        public void DeleteDocument(string name)
        {
            if(string.IsNullOrEmpty(name)) return;
            Database.Products.DeleteDocuments(name);
        }
    }
}
