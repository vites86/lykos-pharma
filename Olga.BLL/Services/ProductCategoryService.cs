using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.BLL.Services
{
    public class ProductCategoryService : IProductCategory
    {
        IUnitOfWorkGeneral Database { get; set; }

        public ProductCategoryService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ProductCategoryDTO productCategoryDto)
        {
            var ProductCategoryDTO = Mapper.Map<ProductCategoryDTO, ProductCategory>(productCategoryDto);
            Database.ProductCategories.Create(ProductCategoryDTO);
        }

        public ProductCategoryDTO GetItem(int id)
        {
            return Mapper.Map<ProductCategory, ProductCategoryDTO>(Database.ProductCategories.Get(id));
        }

        public IEnumerable<ProductCategoryDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryDTO>>(Database.ProductCategories.GetAll().ToList());
        }

        public IEnumerable<ProductCategoryDTO> GetItems(int countryId)
        {
            return Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryDTO>>(Database.ProductCategories.GetAll(countryId).ToList());
        }

        public void DeleteItem(int id)
        {
            Database.ProductCategories.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.ProductCategories.Commit();
        }
    }
}

