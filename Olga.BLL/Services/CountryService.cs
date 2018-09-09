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
    public class CountryService : ICountry
    {
        IUnitOfWorkGeneral Database { get; set; }

        public CountryService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(CountryDTO countryDto)
        {
            var CountryDto = Mapper.Map<CountryDTO, Country>(countryDto);
            Database.Countries.Create(CountryDto);
        }

        public CountryDTO GetItem(int id)
        {
            var country = Database.Countries.Get(id);
            return Mapper.Map<Country, CountryDTO>(country);
        }

        public IEnumerable<CountryDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(Database.Countries.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.Countries.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Commit()
        {
            Database.Countries.Commit();
        }
    }
}
