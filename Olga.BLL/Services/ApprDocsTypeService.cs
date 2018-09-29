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
    public class ApprDocsTypeService : IApprDocsType
    {
        IUnitOfWorkGeneral Database { get; set; }

        public ApprDocsTypeService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ApprDocsTypeDTO apprDocsTypeDTO)
        {
            var apprDocsTypeDto = Mapper.Map<ApprDocsTypeDTO, ApprDocsType>(apprDocsTypeDTO);
            Database.ApprDocsTypes.Create(apprDocsTypeDto);
        }

        public ApprDocsTypeDTO GetItem(int id)
        {
            return Mapper.Map<ApprDocsType, ApprDocsTypeDTO>(Database.ApprDocsTypes.Get(id));
        }

        public IEnumerable<ApprDocsTypeDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<ApprDocsType>, IEnumerable<ApprDocsTypeDTO>>(Database.ApprDocsTypes.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.ApprDocsTypes.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Commit()
        {
            Database.ApprDocsTypes.Commit();
        }
    }
}
