using System;
using System.Collections.Generic;
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
     public class ProcedureService : IProcedure
    {

        private ProcedureRepository Database { get; set; }

        public ProcedureService(ProductContext context)
        {
            Database = new ProcedureRepository(context);
        }

        public void AddItem(ProcedureDTO item)
        {
            Procedure procedure = Mapper.Map<ProcedureDTO, Procedure>(item);
            Database.Create(procedure);
        }

        public ProcedureDTO GetItem(int id)
        {
            return Mapper.Map<Procedure, ProcedureDTO>(Database.Get(id));
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

        public IEnumerable<ProcedureDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(Database.GetAll()).ToArray();
        }

        public IEnumerable<ProcedureDTO> GetItems(int productId)
        {
            return Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(Database.GetAll(productId).ToArray());  
        }
       
    }
}
