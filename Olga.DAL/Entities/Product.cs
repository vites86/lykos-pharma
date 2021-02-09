using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Olga.DAL.Entities
{
    [Table("Products", Schema = "product")]
    public class Product
    {
        public Product()
        {
            this.Artworks = new HashSet<Artwork>();
            this.Manufacturers = new HashSet<Manufacturer>();
            this.ProductDocuments = new HashSet<ProductDocument>();
            this.Procedures = new HashSet<Procedure>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime? IssuedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
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

        public virtual ICollection<Artwork> Artworks { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<ProductDocument> ProductDocuments { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public string Gtin { get; set; }

        public virtual ProductStatus ProductStatus { get; set; }
        public int? ProductStatusId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
