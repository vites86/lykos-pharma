using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities;

namespace Olga.DAL.Interfaces
{
    public interface IUnitOfWorkGeneral : IDisposable
    {
        IProductRepository<Product> Products { get; }
        IRepository<Procedure> Procedures { get; }
        void Save();
    }
}
