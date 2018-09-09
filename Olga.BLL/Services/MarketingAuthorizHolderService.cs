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
    public class MarketingAuthorizHolderService : IMarketingAuthorizHolder
    {
        IUnitOfWorkGeneral Database { get; set; }

        public MarketingAuthorizHolderService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(MarketingAuthorizHolderDTO marketingAuthorizHolderDto)
        {
            var MarketingAuthorizHolderDto = Mapper.Map<MarketingAuthorizHolderDTO, MarketingAuthorizHolder>(marketingAuthorizHolderDto);
            Database.MarketingAuthorizHolders.Create(MarketingAuthorizHolderDto);
        }

        public MarketingAuthorizHolderDTO GetItem(int id)
        {
            return Mapper.Map<MarketingAuthorizHolder, MarketingAuthorizHolderDTO>(Database.MarketingAuthorizHolders.Get(id));
        }

        public IEnumerable<MarketingAuthorizHolderDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<MarketingAuthorizHolder>, IEnumerable<MarketingAuthorizHolderDTO>>(Database.MarketingAuthorizHolders.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.MarketingAuthorizHolders.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.MarketingAuthorizHolders.Commit();
        }

    }
}
