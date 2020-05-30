using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class CountrySettingRepository : IRepository<CountrySetting>
    {
        private ProductContext db;

        public CountrySettingRepository(ProductContext db)
        {
            this.db = db;
        }

        public IQueryable<CountrySetting> GetAll()
        {
            return db.CountrySettings;
        }

        public IQueryable<CountrySetting> GetAll(int countryId)
        {
            return db.CountrySettings;
        }

        public CountrySetting Get(int id)
        {
            return db.CountrySettings.FirstOrDefault(a=>a.CountryId == id);

        }

        public void Create(CountrySetting countrySetting)
        {
            db.CountrySettings.Add(countrySetting);
        }

        public void Update(CountrySetting item)
        {
            var existingCountrySetting = db.CountrySettings.FirstOrDefault(a => a.CountryId == item.CountryId);

            if (existingCountrySetting == null) return;

            existingCountrySetting.EanActive = item.EanActive;
            existingCountrySetting.GtinActive = item.GtinActive;
            Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
