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

        [Display(Name = "Issued Date")]
        public DateTime? IssuedDate { get; set; }
        [Display(Name = "Expiry Date")]
        public DateTime? ExpiredDate { get; set; }

        public string Country { get; set; }

        [Display(Name = "Marketing Authorize Holder")]
        public string MarketingAuthorizHolder { get; set; }

        [Display(Name = "Marketing Authorize Number")]
        public string MarketingAuthorizNumber { get; set; }

        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }

        [Display(Name = "Pharmaceutical Form")]
        public string PharmaceuticalForm { get; set; }

        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string Strength { get; set; }

        public string DocumentImagesListString { get; set; }
        public IList<string> DocumentImages = new List<string>();
        public IList<ProductDocument> ProductDocuments = new List<ProductDocument>();

        public virtual ICollection<ArtworkDTO> Artworks { get; set; }
        public virtual ICollection<ManufacturerDTO> Manufacturers { get; set; }
    }

    public class ProductCreateModel
    {
        public ProductCreateModel()
        {
            this.Artworks = new List<ArtworkViewModel>();
            this.Manufacturers = new List<ManufacturerViewModel>();
            this.DocumentImagesApprs = new List<string>();
            this.DocumentImagesArtworks = new List<string>();
            this.Procedures = new List<ProcedureViewModel>();
            this.Documents = new List<ProductDocument>();
        }

        public int? Id { get; set; }

        [Display(Name = "Issued Date")]
        public DateTime? IssuedDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiredDate { get; set; }

        public virtual Country Country { get; set; }
        public int? CountryId { get; set; }

        [Display(Name = "Marketing Authorize Holder")]
        public virtual MarketingAuthorizHolder MarketingAuthorizHolder { get; set; }
        public int? MarketingAuthorizHolderId { get; set; }

        [Display(Name = "Marketing Authorize Number")]
        public virtual MarketingAuthorizNumber MarketingAuthorizNumber { get; set; }
        public int? MarketingAuthorizNumberId { get; set; }

        [Display(Name = "Pack Size")]
        public virtual PackSize PackSize { get; set; }
        public int? PackSizeId { get; set; }

        [Display(Name = "Pharmaceutical Form")]
        public virtual PharmaceuticalForm PharmaceuticalForm { get; set; }
        public int? PharmaceuticalFormId { get; set; }

        [Display(Name = "Product Code")]
        public virtual ProductCode ProductCode { get; set; }
        public int? ProductCodeId { get; set; }

        [Display(Name = "Product Name")]
        public virtual ProductName ProductName { get; set; }
        public int? ProductNameId { get; set; }

        public virtual Strength Strength { get; set; }
        public int? StrengthId { get; set; }

        public List<ArtworkViewModel> Artworks { get; set; }
        public List<ManufacturerViewModel> Manufacturers { get; set; }

        public string DocumentImagesListStringApprs { get; set; }
        public List<string> DocumentImagesApprs { get; set; }

        public string DocumentImagesListStringArtworks { get; set; }
        public List<string> DocumentImagesArtworks { get; set; }

        public List<ProcedureViewModel> Procedures { get; set; }
        public List<ProductDocument> Documents { get; set; }
    }
}