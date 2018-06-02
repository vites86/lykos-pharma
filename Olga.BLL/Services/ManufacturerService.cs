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
public class ManufacturerService: IManufacturer
    {

        private ManufacturerRepository Database { get; set; }

        public ManufacturerService(ProductContext context)
        {
            Database = new ManufacturerRepository(context);
        }

        public void AddItem(ManufacturerDTO manufacturerDto)
        {
            var ManufacturerDto = Mapper.Map<ManufacturerDTO, Manufacturer>(manufacturerDto);
            Database.Create(ManufacturerDto);
        }

        public ManufacturerDTO GetItem(int id)
        {
            return Mapper.Map<Manufacturer, ManufacturerDTO>(Database.Get(id));
        }

        public IEnumerable<ManufacturerDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerDTO>>(Database.GetAll().ToList());
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
