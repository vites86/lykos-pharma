using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Olga.BLL.Interfaces;
using Olga.BLL.DTO;
using Olga.DAL.Entities;

namespace Olga.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public DateTime? IssuedDate { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public virtual ICollection<ArtworkDTO> Artworks { get; set; }
        public virtual ICollection<ApprDocsTypeDTO> ApprDocsTypes { get; set; }
        public virtual ICollection<ManufacturerDTO> Manufacturers { get; set; }

        public string Country { get; set; }

        public string MarketingAuthorizHolder { get; set; }

        public string MarketingAuthorizNumber { get; set; }

        public string PackSize { get; set; }

        public string PharmaceuticalForm { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string Strength { get; set; }

        public string DocumentImagesListString { get; set; }
        public IList<string> DocumentImages = new List<string>();
        public IList<ProductDocument> ProductDocuments = new List<ProductDocument>();
    }

    public class ProductCreateModel
    {
        public ProductCreateModel()
        {
            this.Artworks = new List<ArtworkViewModel>();
            this.Manufacturers = new List<ManufacturerViewModel>();
            this.ApprDocsTypes = new List<ApprDocsTypeViewModel>();
            this.DocumentImages = new List<string>();
        }

        public int? Id { get; set; }

        public DateTime? IssuedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }

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

        public List<ApprDocsTypeViewModel> ApprDocsTypes { get; set; }
        public List<ArtworkViewModel> Artworks { get; set; }
        public List<ManufacturerViewModel> Manufacturers { get; set; }

        public string DocumentImagesListString { get; set; }
        public List<string> DocumentImages { get; set; }


    }
}