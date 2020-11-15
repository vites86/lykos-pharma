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
    public class ProductStatusService: IProductStatus
    {
        IUnitOfWorkGeneral Database { get; set; }

        public ProductStatusService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public IEnumerable<ProductStatusDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<ProductStatus>, IEnumerable<ProductStatusDTO>>(Database.ProductStatuses.GetAll().ToList());
        }

        public void AddItem(ProductStatusDTO productStatusDTO)
        {
            var ProductStatusDTO = Mapper.Map<ProductStatusDTO, ProductStatus>(productStatusDTO);
            Database.ProductStatuses.Create(ProductStatusDTO);
        }

        public ProductStatusDTO GetItem(int id)
        {
            return Mapper.Map<ProductStatus, ProductStatusDTO>(Database.ProductStatuses.Get(id));
        }

        public void DeleteItem(int id)
        {
            Database.ProductStatuses.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.ProductStatuses.Commit();
        }
    }
}
