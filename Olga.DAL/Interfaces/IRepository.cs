using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities;

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

    public interface IProductRepository<T>: IRepository<T> where T : class
    {
        void Create(T product, string[] selectedManufacturers, string[] selectedArtworks);
        void Update(T product, string[] selectedManufacturers, string[] selectedArtworks);
        ApprDocsType GetApprDocsType(int id);
        Artwork GetArtwork(int id);
        Manufacturer GetManufacturer(int id);
        void DeleteDocuments(string fileName);
        Task<T> FindAsync(int id);
    }

    public interface IProcedureRepository<T> : IRepository<T> where T : class
    {
        void DeleteDocument(string fileName);
    }
}
