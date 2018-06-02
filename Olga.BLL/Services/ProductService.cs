using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private ProductRepository Database { get; set; }

        public ProductService(ProductContext context)
        {
           Database = new ProductRepository(context);
        }

        public  void AddProduct(ProductDTO productDto, string[] selectedApprDocsTypes, string[] selectedManufacturers, string[] selectedArtworks)
        {
            Product product = Mapper.Map<ProductDTO, Product>(productDto);
            Database.Create(product, selectedApprDocsTypes, selectedManufacturers, selectedArtworks);
        }

        public ProductDTO GetProduct(int id)
        {
            return Mapper.Map<Product,ProductDTO > (Database.Get(id));
        }

        public IEnumerable<ProductDTO> GetProducts(int? countryId)
        {
            if (countryId == null)
            {
               return null;
            }

            var productList = Database.GetAll((int)countryId).ToList();
            if (productList.Any())
            {
                return Mapper.Map<List<Product>, List<ProductDTO>>(productList.ToList()); 
            }

            return null;
        }

        public ApprDocsTypeDTO GetApprDocsType(int id)
        {
            return Mapper.Map<ApprDocsType, ApprDocsTypeDTO>(Database.GetApprDocsType(id));
        }

        public ArtworkDTO GetArtwork(int id)
        {
            return Mapper.Map<Artwork, ArtworkDTO>(Database.GetArtwork(id));
        }

        public ManufacturerDTO GetManufacturer(int id)
        {
            return Mapper.Map<Manufacturer, ManufacturerDTO>(Database.GetManufacturer(id)); 
        }

        public void DeleteProduct(int id)
        {
            Database.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.Commit();
        }
    }
}
