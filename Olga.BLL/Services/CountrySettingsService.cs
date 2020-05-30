using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.BLL.Services
{
    public class CountrySettingsService: IBase<CountrySettingDTO>
    {
        IUnitOfWorkGeneral Database { get; set; }
        public CountrySettingsService(IUnitOfWorkGeneral database)
        {
            Database = database;
        }

        public void AddItem(CountrySettingDTO item)
        {
            if (item.CountryId == null)
            {
                return;
            }
            var countrySetting = Mapper.Map<CountrySettingDTO, CountrySetting>(item);
            var countryId = (int) item.CountryId;
            var setting = Database.CountrySettings.Get(countryId);

            if (setting == null)
            {
                Database.CountrySettings.Create(countrySetting);
            }
            else
            {
                Database.CountrySettings.Update(countrySetting);
            }
        }

        public CountrySettingDTO GetItem(int id)
        {
            return Mapper.Map<CountrySetting, CountrySettingDTO>(Database.CountrySettings.Get(id));
        }

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Commit()
        {
            Database.CountrySettings.Commit();
        }

        public IEnumerable<CountrySettingDTO> GetItems()
        {
            var countrySettings = Database.CountrySettings.GetAll().ToList();
            return Mapper.Map<IEnumerable<CountrySetting>, IEnumerable<CountrySettingDTO>>(countrySettings);
        }
    }
}
