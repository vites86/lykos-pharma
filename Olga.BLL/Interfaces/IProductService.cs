using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Olga.BLL.DTO;
using Olga.DAL.Entities;
using System.Web;

namespace Olga.BLL.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDTO productDto, string[] selectedManufacturers, string[] selectedArtworks);
        ProductDTO GetProduct(int id);
        void DeleteProduct(int id);
        IEnumerable<ProductDTO> GetProducts(int? countryId);
        void Dispose();
        void Commit();
        ApprDocsTypeDTO GetApprDocsType(int id);
        ArtworkDTO GetArtwork(int id);
        ManufacturerDTO GetManufacturer(int id);
        void DeleteDocument(string name);
        void DeleteAdditionalDocument(string productId, string documentId);
        Task<bool> AddFileToProd(string localFileName, int productId, string prodDocType);
    }
}
