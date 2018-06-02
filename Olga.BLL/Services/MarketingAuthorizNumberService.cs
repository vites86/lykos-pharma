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
public class MarketingAuthorizNumberService: IMarketingAuthorizNumber
    {

        private MarketingAuthorizNumberRepository Database { get; set; }

        public MarketingAuthorizNumberService(ProductContext context)
        {
            Database = new MarketingAuthorizNumberRepository(context);
        }

        public void AddItem(MarketingAuthorizNumberDTO marketingAuthorizNumberDto)
        {
            var MarketingAuthorizNumberDto = Mapper.Map<MarketingAuthorizNumberDTO, MarketingAuthorizNumber>(marketingAuthorizNumberDto);
            Database.Create(MarketingAuthorizNumberDto);
        }

        public MarketingAuthorizNumberDTO GetItem(int id)
        {
            return Mapper.Map<MarketingAuthorizNumber, MarketingAuthorizNumberDTO>(Database.Get(id));
        }

        public IEnumerable<MarketingAuthorizNumberDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<MarketingAuthorizNumber>, IEnumerable<MarketingAuthorizNumberDTO>>(Database.GetAll().ToList());
        }

        public IEnumerable<MarketingAuthorizNumberDTO> GetItems(int countryId)
        {
            return Mapper.Map<IEnumerable<MarketingAuthorizNumber>, IEnumerable<MarketingAuthorizNumberDTO>>(Database.GetAll(countryId).ToList());
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
