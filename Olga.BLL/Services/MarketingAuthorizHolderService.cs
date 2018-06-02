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
public class  MarketingAuthorizHolderService: IMarketingAuthorizHolder
    {

        private  MarketingAuthorizHolderRepository Database { get; set; }

        public  MarketingAuthorizHolderService(ProductContext context)
        {
            Database = new  MarketingAuthorizHolderRepository(context);
        }

        public void AddItem( MarketingAuthorizHolderDTO  marketingAuthorizHolderDto)
        {
            var  MarketingAuthorizHolderDto = Mapper.Map< MarketingAuthorizHolderDTO,  MarketingAuthorizHolder>( marketingAuthorizHolderDto);
            Database.Create( MarketingAuthorizHolderDto);
        }

        public  MarketingAuthorizHolderDTO GetItem(int id)
        {
            return Mapper.Map< MarketingAuthorizHolder,  MarketingAuthorizHolderDTO>(Database.Get(id));
        }

        public IEnumerable< MarketingAuthorizHolderDTO> GetItems()
        {
            return Mapper.Map<IEnumerable< MarketingAuthorizHolder>, IEnumerable< MarketingAuthorizHolderDTO>>(Database.GetAll().ToList());
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
