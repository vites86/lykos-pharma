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
    public class StrengthService : IStrength
    {

        IUnitOfWorkGeneral Database { get; set; }
        //private StrengthRepository Database { get; set; }

        public StrengthService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(StrengthDTO strengthDto)
        {
            var StrengthDto = Mapper.Map<StrengthDTO, Strength>(strengthDto);
            Database.Strengths.Create(StrengthDto);
        }

        public StrengthDTO GetItem(int id)
        {
            return Mapper.Map<Strength, StrengthDTO>(Database.Strengths.Get(id));
        }

        public IEnumerable<StrengthDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Strength>, IEnumerable<StrengthDTO>>(Database.Strengths.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.Strengths.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.Strengths.Commit();
        }

    }
}
