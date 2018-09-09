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
    public class PharmaceuticalFormService : IPharmaceuticalForm
    {
        IUnitOfWorkGeneral Database { get; set; }

        public PharmaceuticalFormService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(PharmaceuticalFormDTO pharmaceuticalFormDto)
        {
            var PharmaceuticalFormDto = Mapper.Map<PharmaceuticalFormDTO, PharmaceuticalForm>(pharmaceuticalFormDto);
            Database.PharmaceuticalForms.Create(PharmaceuticalFormDto);
        }

        public PharmaceuticalFormDTO GetItem(int id)
        {
            return Mapper.Map<PharmaceuticalForm, PharmaceuticalFormDTO>(Database.PharmaceuticalForms.Get(id));
        }

        public IEnumerable<PharmaceuticalFormDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<PharmaceuticalForm>, IEnumerable<PharmaceuticalFormDTO>>(Database.PharmaceuticalForms.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.PharmaceuticalForms.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.PharmaceuticalForms.Commit();
        }

    }
}
