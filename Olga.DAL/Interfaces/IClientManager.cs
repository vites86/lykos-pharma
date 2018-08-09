using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities.Account;

namespace Olga.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void CreateClientProfile(ClientProfile item);
        void DeleteClientProfile(string id);
    }
}
