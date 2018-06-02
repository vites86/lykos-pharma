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
using Olga.DAL.Repositories;

namespace Olga.BLL.Services
{
public class PackSizeService: IPackSize
    {

        private PackSizeRepository Database { get; set; }

        public PackSizeService(ProductContext context)
        {
            Database = new PackSizeRepository(context);
        }

        public void AddItem(PackSizeDTO packSizeDto)
        {
            var PackSizeDto = Mapper.Map<PackSizeDTO, PackSize>(packSizeDto);
            Database.Create(PackSizeDto);
        }

        public PackSizeDTO GetItem(int id)
        {
            return Mapper.Map<PackSize, PackSizeDTO>(Database.Get(id));
        }

        public IEnumerable<PackSizeDTO> GetItems(int countryId)
        {
            return Mapper.Map<IEnumerable<PackSize>, IEnumerable<PackSizeDTO>>(Database.GetAll(countryId).ToList());

        }

        public IEnumerable<PackSizeDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<PackSize>, IEnumerable<PackSizeDTO>>(Database.GetAll().ToList());
        }

        public void DeleteItem(int id)
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
