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
using Olga.DAL.Interfaces;
using Olga.DAL.Repositories;

namespace Olga.BLL.Services
{
     public class ProcedureService : IProcedure
    {
        IUnitOfWorkGeneral Database { get; set; }

        public ProcedureService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ProcedureDTO item)
        {
            Procedure procedure = Mapper.Map<ProcedureDTO, Procedure>(item);
            Database.Procedures.Create(procedure);
        }

        public ProcedureDTO GetItem(int id)
        {
            return Mapper.Map<Procedure, ProcedureDTO>(Database.Procedures.Get(id));
        }

        public void DeleteItem(int id)
        {
            Database.Procedures.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Commit()
        {
            Database.Procedures.Commit();
        }

        public IEnumerable<ProcedureDTO> GetItems()
        {
            var procedures = Database.Procedures.GetAll().ToArray();
            return Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(procedures);
        }

        public IEnumerable<ProcedureDTO> GetItems(int productId)
        {
            return Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(Database.Procedures.GetAll(productId).ToArray());  
        }

        public void Update(ProcedureDTO procedureDto)
        {
            var procedure = Mapper.Map<ProcedureDTO, Procedure>(procedureDto);
            Database.Procedures.Update(procedure);
        }

        public void DeleteDocument(ProcedureDTO procedureDto)
        {
            var procedure = Mapper.Map<ProcedureDTO, Procedure>(procedureDto);
            Database.Procedures.Update(procedure);
        }


    }
}
