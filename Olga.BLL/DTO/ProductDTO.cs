using System;
using System.Collections.Generic;
using Olga.DAL.Entities;

namespace Olga.BLL.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            this.Artworks = new List<ArtworkDTO>();
            this.Manufacturers = new List<ManufacturerDTO>();
            this.ProductDocuments = new List<ProductDocument>();
            this.Procedures = new List<ProcedureDTO>();
        }

        public int Id { get; set; }

        public DateTime? IssuedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool? UnLimited { get; set; }

        public virtual Country Country { get; set; }
        public int? CountryId { get; set; }
       
        public virtual MarketingAuthorizHolder MarketingAuthorizHolder { get; set; }
        public int? MarketingAuthorizHolderId { get; set; }

        public virtual MarketingAuthorizNumber MarketingAuthorizNumber { get; set; }
        public int? MarketingAuthorizNumberId { get; set; }

        public virtual PackSize PackSize { get; set; }
        public int? PackSizeId { get; set; }

        public virtual PharmaceuticalForm PharmaceuticalForm { get; set; }
        public int? PharmaceuticalFormId { get; set; }

        public virtual ProductCode ProductCode { get; set; }
        public int? ProductCodeId { get; set; }

        public virtual ProductName ProductName { get; set; }
        public int? ProductNameId { get; set; }

        public virtual Strength Strength { get; set; }
        public int? StrengthId { get; set; } 

        public List<ArtworkDTO> Artworks { get; set; }
        public List<ManufacturerDTO> Manufacturers { get; set; }
        public List<ProductDocument> ProductDocuments { get; set; }
        public List<ProcedureDTO> Procedures { get; set; }
        public string Gtin { get; set; }

        public virtual ProductStatus ProductStatus { get; set; }
        public int? ProductStatusId { get; set; }
    }
}
