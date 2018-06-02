using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.BLL.DTO;
using Olga.DAL.Entities;

namespace Olga.BLL.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDTO productDto, string[] selectedApprDocsTypes, string[] selectedManufacturers, string[] selectedArtworks);
        ProductDTO GetProduct(int id);
        void DeleteProduct(int id);
        IEnumerable<ProductDTO> GetProducts(int? countryId);
        void Dispose();
        void Commit();
        ApprDocsTypeDTO GetApprDocsType(int id);
        ArtworkDTO GetArtwork(int id);
        ManufacturerDTO GetManufacturer(int id);

    }
}
