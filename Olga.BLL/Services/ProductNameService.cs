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
    public class ProductNameService : IProductName
    {
        IUnitOfWorkGeneral Database { get; set; }

        public ProductNameService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ProductNameDTO productNameDto)
        {
            var productName = Mapper.Map<ProductNameDTO, ProductName>(productNameDto);
            Database.ProductNames.Create(productName);
        }

        public ProductNameDTO GetItem(int id)
        {
            return Mapper.Map<ProductName, ProductNameDTO>(Database.ProductNames.Get(id));
        }

        public IEnumerable<ProductNameDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<ProductName>, IEnumerable<ProductNameDTO>>(Database.ProductNames.GetAll().ToList());
        }

        public IEnumerable<ProductNameDTO> GetItems(int countryId)
        {
            return Mapper.Map<IEnumerable<ProductName>, IEnumerable<ProductNameDTO>>(Database.ProductNames.GetAll(countryId).ToList());
        }

        public void DeleteItem(int id)
        {
            Database.ProductNames.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.ProductNames.Commit();
        }

    }
}
