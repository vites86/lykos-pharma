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
    public class ProductCodeService : IProductCode
    {

        IUnitOfWorkGeneral Database { get; set; }

        public ProductCodeService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ProductCodeDTO productCodeDto)
        {
            var ProductCodeDto = Mapper.Map<ProductCodeDTO, ProductCode>(productCodeDto);
            Database.ProductCodes.Create(ProductCodeDto);
        }

        public ProductCodeDTO GetItem(int id)
        {
            return Mapper.Map<ProductCode, ProductCodeDTO>(Database.ProductCodes.Get(id));
        }

        public IEnumerable<ProductCodeDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<ProductCode>, IEnumerable<ProductCodeDTO>>(Database.ProductCodes.GetAll().ToList());
        }

        public IEnumerable<ProductCodeDTO> GetItems(int countryId)
        {
            return Mapper.Map<IEnumerable<ProductCode>, IEnumerable<ProductCodeDTO>>(Database.ProductCodes.GetAll(countryId).ToList());
        }

        public void DeleteItem(int id)
        {
            Database.ProductCodes.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.ProductCodes.Commit();
        }
    }
}
