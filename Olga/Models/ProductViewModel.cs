using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
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

        [NotMapped]
        public bool IsFired { get; set; }

        public ProductViewModel()
        {
            if (ExpiredDate != null)
            {
                DateTime yearAndHalf = DateTime.Now.AddMonths(18);
                int result = DateTime.Compare((DateTime)ExpiredDate, yearAndHalf);
                IsFired = result < 0;
            }
        }


        [Display(Name = "Issued Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? IssuedDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiredDate { get; set; }

        public bool? UnLimited { get; set; }

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
        public List<ProductDocument> ProductDocuments = new List<ProductDocument>();

        public virtual List<ArtworkDTO> Artworks { get; set; }
        public virtual List<ManufacturerDTO> Manufacturers { get; set; }
        public virtual List<ProcedureDTO> Procedures { get; set; }

        [Display(Name = "GTIN")]
        public string Gtin { get; set; }
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
            this.DocumentListStringGtin = new List<ProductDocument>();
            this.DocumentListStringEan = new List<ProductDocument>();
            this.DocumentListStringGmp = new List<ProductDocument>();
        }

        public int? Id { get; set; }

        [Display(Name = "Issued Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? IssuedDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiredDate { get; set; }
        public bool UnLimited { get; set; }
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

        [Display(Name = "GTIN")]
        public string DocumentImagesListStringGtin { get; set; }
        public List<ProductDocument> DocumentListStringGtin { get; set; }

        [Display(Name = "EAN")]
        public string DocumentImagesListStringEan { get; set; }
        public List<ProductDocument> DocumentListStringEan { get; set; }

        public List<ProcedureViewModel> Procedures { get; set; }
        public List<ProductDocument> Documents { get; set; }

        [Display(Name = "GTIN")]
        public string Gtin { get; set; }

        [Display(Name = "GMP conclusion")]
        public string DocumentImagesListStringGmp { get; set; }
        public List<ProductDocument> DocumentListStringGmp { get; set; }

    }

    public class ShowProductModel
    {
        public ShowProductModel()
        {
            this.Artworks = new List<ArtworkViewModel>();
            this.Manufacturers = new List<ManufacturerViewModel>();
            this.DocumentImagesApprs = new List<string>();
            this.DocumentImagesArtworks = new List<string>();
            this.Procedures = new List<ProcedureViewModel>();
            this.Documents = new List<ProductDocument>();
            this.DocumentListStringGtin = new List<ProductDocument>();
            this.DocumentListStringEan = new List<ProductDocument>();
            this.DocumentListStringGmp = new List<ProductDocument>();
        }

        public int? Id { get; set; }

        public string IssuedDate { get; set; }
        public string ExpiredDate { get; set; }

        public bool UnLimited { get; set; }

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
        public string  ProductCode { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string  Strength { get; set; }

        public List<ArtworkViewModel> Artworks { get; set; }
        public List<ManufacturerViewModel> Manufacturers { get; set; }

        public string DocumentImagesListStringApprs { get; set; }
        public List<string> DocumentImagesApprs { get; set; }

        public string DocumentImagesListStringArtworks { get; set; }
        public List<string> DocumentImagesArtworks { get; set; }

        public List<ProcedureViewModel> Procedures { get; set; }
        public List<ProductDocument> Documents { get; set; }

        [Display(Name = "GTIN")]
        public string DocumentImagesListStringGtin { get; set; }
        public List<ProductDocument> DocumentListStringGtin { get; set; }

        [Display(Name = "EAN")]
        public string DocumentImagesListStringEan { get; set; }
        public List<ProductDocument> DocumentListStringEan { get; set; }

        [Display(Name = "GTIN")]
        public string Gtin { get; set; }

        [Display(Name = "GMP conclusion")]
        public string DocumentImagesListStringGmp { get; set; }
        public List<ProductDocument> DocumentListStringGmp { get; set; }
    }

    public class ProductCompareModel
    {
        [IgnoreDataMember]
        public int? Id { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool UnLimited { get; set; }
        [IgnoreDataMember]
        public virtual Country Country { get; set; }
        public int? CountryId { get; set; }
        public int? MarketingAuthorizHolderId { get; set; }
        public int? MarketingAuthorizNumberId { get; set; }
        public int? PackSizeId { get; set; }
        public int? PharmaceuticalFormId { get; set; }
        public int? ProductCodeId { get; set; }
        public int? ProductNameId { get; set; }
        public int? StrengthId { get; set; }
    }

    public class ProductAdditionalDocsModel
    {
        public ProductAdditionalDocsModel()
        {
            this.Documents = new List<ProductDocument>();
            this.DocumentListStringGtin = new List<ProductDocument>();
            this.DocumentListStringEan = new List<ProductDocument>();
        }

        public int Id { get; set; }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public int CountryId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Pharmaceutical Form")]
        public string PharmaceuticalForm { get; set; }

        [Display(Name = "Strength")]
        public string Strength { get; set; }

        [Display(Name = "Marketing Authorize Number")]
        public string MarketingAuthorizNumber { get; set; }

        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Display(Name = "GTIN")]
        public List<ProductDocument> DocumentListStringGtin { get; set; }

        [Display(Name = "EAN")]
        public List<ProductDocument> DocumentListStringEan { get; set; }

        [Display(Name = "Documents")]
        public List<ProductDocument> Documents { get; set; }

        [Display(Name = "GTIN")]
        public string Gtin { get; set; }

        [Display(Name = "GMP conclusion")]
        public List<ProductDocument> DocumentListStringGmp { get; set; }

    }
}