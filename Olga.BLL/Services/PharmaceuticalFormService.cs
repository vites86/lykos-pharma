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
public class PharmaceuticalFormService: IPharmaceuticalForm
    {

        private PharmaceuticalFormRepository Database { get; set; }

        public PharmaceuticalFormService(ProductContext context)
        {
            Database = new PharmaceuticalFormRepository(context);
        }

        public void AddItem(PharmaceuticalFormDTO pharmaceuticalFormDto)
        {
            var PharmaceuticalFormDto = Mapper.Map<PharmaceuticalFormDTO, PharmaceuticalForm>(pharmaceuticalFormDto);
            Database.Create(PharmaceuticalFormDto);
        }

        public PharmaceuticalFormDTO GetItem(int id)
        {
            return Mapper.Map<PharmaceuticalForm, PharmaceuticalFormDTO>(Database.Get(id));
        }

        public IEnumerable<PharmaceuticalFormDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<PharmaceuticalForm>, IEnumerable<PharmaceuticalFormDTO>>(Database.GetAll().ToList());
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
