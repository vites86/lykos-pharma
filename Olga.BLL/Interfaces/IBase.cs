using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.BLL.DTO;
using Olga.DAL.Entities;

namespace Olga.BLL.Interfaces
{
    public interface IBase<T> where T : class
    {
        void AddItem(T item);
        T GetItem(int id);
        void DeleteItem(int id);
        void Dispose();
        void Commit();
        //IEnumerable<T> GetItems(int countryId);
        IEnumerable<T> GetItems();
    }

    public interface IProcedure : IPagination<ProcedureDTO>, IBase<ProcedureDTO>
    {
        //IEnumerable<ProcedureDTO> GetItems();
        IEnumerable<ProcedureDTO> GetItems(int productId);
        void Update(ProcedureDTO procedure);
        void DeleteDocument(string fileName);
        IEnumerable<ProcedureDTO> GetPaginated(int? countryId, string searchValue, string sortOrder, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered);
        IEnumerable<ProcedureDTO> GetProceduresOptimized(int? countryId, string searchValue, string sortColumnName, string sortDirection, int start, int length, out int totalrows, out int totalrowsafterfiltering);
        //void UpdateDocument(ProcedureDTO procedure);
    }

    public interface IApprDocsType : IBase<ApprDocsTypeDTO>
    {
        //IEnumerable<ApprDocsTypeDTO> GetItems();
    }

    public interface IArtwork : IBase<ArtworkDTO>
    {
       // IEnumerable<ArtworkDTO> GetItems();
    }

    public interface ICountry : IBase<CountryDTO>
    {
        List<string> GetCountryUsersEmails(int id);
        List<string> GetCountryUsersEmailsViaName(string countryName);
        //IEnumerable<CountryDTO> GetItems();
    }

    public interface IManufacturer : IBase<ManufacturerDTO>
    {
        //IEnumerable<ManufacturerDTO> GetItems();
    }

    public interface IMarketingAuthorizHolder : IBase<MarketingAuthorizHolderDTO>
    {
        //IEnumerable<MarketingAuthorizHolderDTO> GetItems();
    }

    public interface IMarketingAuthorizNumber : IBase<MarketingAuthorizNumberDTO>
    {
        IEnumerable<MarketingAuthorizNumberDTO> GetItems(int countryId);
        //IEnumerable<MarketingAuthorizNumberDTO> GetItems();
    }

    public interface IPackSize : IBase<PackSizeDTO>
    {
        //IEnumerable<PackSizeDTO> GetItems();
        IEnumerable<PackSizeDTO> GetItems(int countryId);
    }

    public interface IPharmaceuticalForm : IBase<PharmaceuticalFormDTO>
    {
        //IEnumerable<PharmaceuticalFormDTO> GetItems();
    }


    public interface IProductCode : IBase<ProductCodeDTO>
    {
        //IEnumerable<ProductCodeDTO> GetItems();
        IEnumerable<ProductCodeDTO> GetItems(int countryId);
    }


    public interface IProductName : IBase<ProductNameDTO>
    {
       // IEnumerable<ProductNameDTO> GetItems();
        IEnumerable<ProductNameDTO> GetItems(int countryId);
    }

    public interface IStrength : IBase<StrengthDTO>
    {
       // IEnumerable<StrengthDTO> GetItems();
    }

}
