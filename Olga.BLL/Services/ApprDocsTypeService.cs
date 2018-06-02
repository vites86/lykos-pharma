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
public class ApprDocsTypeService: IApprDocsType
    {

        private ApprDocsTypeRepository Database { get; set; }

        public ApprDocsTypeService(ProductContext context)
        {
            Database = new ApprDocsTypeRepository(context);
        }

        public void AddItem(ApprDocsTypeDTO apprDocsTypeDTO)
        {
            var apprDocsTypeDto = Mapper.Map<ApprDocsTypeDTO, ApprDocsType>(apprDocsTypeDTO);
            Database.Create(apprDocsTypeDto);
        }

        public ApprDocsTypeDTO GetItem(int id)
        {
            return Mapper.Map<ApprDocsType, ApprDocsTypeDTO>(Database.Get(id));
        }

        public IEnumerable<ApprDocsTypeDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<ApprDocsType>, IEnumerable<ApprDocsTypeDTO>>(Database.GetAll().ToList());
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
