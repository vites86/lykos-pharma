using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.EF;
using Olga.DAL.Entities.Account;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public AccountContext Database { get; set; }

        public ClientManager(AccountContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Delete(string id)
        {
            var clientProfile = Database.ClientProfiles.Find(id);
            if (clientProfile==null)
            {
                return;
            }
            Database.ClientProfiles.Remove(clientProfile);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
