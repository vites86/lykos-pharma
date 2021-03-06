﻿using System;
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
    public class ManufacturerService : IManufacturer
    {

        IUnitOfWorkGeneral Database { get; set; }
        //private ManufacturerRepository Database { get; set; }

        public ManufacturerService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ManufacturerDTO manufacturerDto)
        {
            var ManufacturerDto = Mapper.Map<ManufacturerDTO, Manufacturer>(manufacturerDto);
            Database.Manufacturers.Create(ManufacturerDto);
        }

        public ManufacturerDTO GetItem(int id)
        {
            return Mapper.Map<Manufacturer, ManufacturerDTO>(Database.Manufacturers.Get(id));
        }

        public IEnumerable<ManufacturerDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerDTO>>(Database.Manufacturers.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.Manufacturers.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.Manufacturers.Commit();
        }
    }
}
