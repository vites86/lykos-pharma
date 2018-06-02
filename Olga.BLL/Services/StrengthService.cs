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
public class StrengthService: IStrength
    {

        private StrengthRepository Database { get; set; }

        public StrengthService(ProductContext context)
        {
            Database = new StrengthRepository(context);
        }

        public void AddItem(StrengthDTO strengthDto)
        {
            var StrengthDto = Mapper.Map<StrengthDTO, Strength>(strengthDto);
            Database.Create(StrengthDto);
        }

        public StrengthDTO GetItem(int id)
        {
            return Mapper.Map<Strength, StrengthDTO>(Database.Get(id));
        }

        public IEnumerable<StrengthDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Strength>, IEnumerable<StrengthDTO>>(Database.GetAll().ToList());
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
