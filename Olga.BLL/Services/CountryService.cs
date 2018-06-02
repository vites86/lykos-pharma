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
public class CountryService: ICountry
    {

        private CountryRepository Database { get; set; }

        public CountryService(ProductContext context)
        {
            Database = new CountryRepository(context);
        }

        public void AddItem(CountryDTO countryDto)
        {
            var CountryDto = Mapper.Map<CountryDTO, Country>(countryDto);
            Database.Create(CountryDto);
        }

        public CountryDTO GetItem(int id)
        {
            var country = Database.Get(id);
            return Mapper.Map<Country, CountryDTO>(country);
        }

        public IEnumerable<CountryDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(Database.GetAll().ToList());
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
