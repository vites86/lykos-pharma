using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Entities.Account;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        //public AccountContext Database { get; set; }
        public ProductContext Database { get; set; }

        public ClientManager(ProductContext db)
        {
            Database = db;
        }

        public void CreateClientProfile(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void DeleteClientProfile(string id)
        {
            var clientProfile = Database.ClientProfiles.Find(id);
            if (clientProfile==null)
            {
                return;
            }
            clientProfile.Countries.Clear();
            Database.ClientProfiles.Remove(clientProfile);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
