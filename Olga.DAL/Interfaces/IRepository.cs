using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.DAL.Interfaces
{
   public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(int countryId);
        T Get(int id);
        void Create(T country);
        void Update(T item);
        void Delete(int id);
        void Commit();
    }
}
