namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationCountryDeps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "info.ApprDocsTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApprType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "info.Artworks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Artwork_name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "info.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "info.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "info.MarketingAuthorizHolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "info.MarketingAuthorizNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("info.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "info.PackSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.String(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("info.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "info.PharmaceuticalForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PharmaForm = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "info.ProductCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("info.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "info.ProductNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("info.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "product.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssuedDate = c.DateTime(),
                        ExpiredDate = c.DateTime(),
                        CountryId = c.Int(),
                        ApprDocsTypeId = c.Int(),
                        ArtworkId = c.Int(),
                        ManufacturerId = c.Int(),
                        MarketingAuthorizHolderId = c.Int(),
                        MarketingAuthorizNumberId = c.Int(),
                        PackSizeId = c.Int(),
                        PharmaceuticalFormId = c.Int(),
                        ProductCodeId = c.Int(),
                        ProductNameId = c.Int(),
                        StrengthId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("info.ApprDocsTypes", t => t.ApprDocsTypeId)
                .ForeignKey("info.Artworks", t => t.ArtworkId)
                .ForeignKey("info.Countries", t => t.CountryId)
                .ForeignKey("info.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("info.MarketingAuthorizHolders", t => t.MarketingAuthorizHolderId)
                .ForeignKey("info.MarketingAuthorizNumbers", t => t.MarketingAuthorizNumberId)
                .ForeignKey("info.PackSizes", t => t.PackSizeId)
                .ForeignKey("info.PharmaceuticalForms", t => t.PharmaceuticalFormId)
                .ForeignKey("info.ProductCodes", t => t.ProductCodeId)
                .ForeignKey("info.ProductNames", t => t.ProductNameId)
                .ForeignKey("info.Strengths", t => t.StrengthId)
                .Index(t => t.CountryId)
                .Index(t => t.ApprDocsTypeId)
                .Index(t => t.ArtworkId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.MarketingAuthorizHolderId)
                .Index(t => t.MarketingAuthorizNumberId)
                .Index(t => t.PackSizeId)
                .Index(t => t.PharmaceuticalFormId)
                .Index(t => t.ProductCodeId)
                .Index(t => t.ProductNameId)
                .Index(t => t.StrengthId);
            
            CreateTable(
                "info.Strengths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Strngth = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("product.Products", "StrengthId", "info.Strengths");
            DropForeignKey("product.Products", "ProductNameId", "info.ProductNames");
            DropForeignKey("product.Products", "ProductCodeId", "info.ProductCodes");
            DropForeignKey("product.Products", "PharmaceuticalFormId", "info.PharmaceuticalForms");
            DropForeignKey("product.Products", "PackSizeId", "info.PackSizes");
            DropForeignKey("product.Products", "MarketingAuthorizNumberId", "info.MarketingAuthorizNumbers");
            DropForeignKey("product.Products", "MarketingAuthorizHolderId", "info.MarketingAuthorizHolders");
            DropForeignKey("product.Products", "ManufacturerId", "info.Manufacturers");
            DropForeignKey("product.Products", "CountryId", "info.Countries");
            DropForeignKey("product.Products", "ArtworkId", "info.Artworks");
            DropForeignKey("product.Products", "ApprDocsTypeId", "info.ApprDocsTypes");
            DropForeignKey("info.ProductNames", "CountryId", "info.Countries");
            DropForeignKey("info.ProductCodes", "CountryId", "info.Countries");
            DropForeignKey("info.PackSizes", "CountryId", "info.Countries");
            DropForeignKey("info.MarketingAuthorizNumbers", "CountryId", "info.Countries");
            DropIndex("product.Products", new[] { "StrengthId" });
            DropIndex("product.Products", new[] { "ProductNameId" });
            DropIndex("product.Products", new[] { "ProductCodeId" });
            DropIndex("product.Products", new[] { "PharmaceuticalFormId" });
            DropIndex("product.Products", new[] { "PackSizeId" });
            DropIndex("product.Products", new[] { "MarketingAuthorizNumberId" });
            DropIndex("product.Products", new[] { "MarketingAuthorizHolderId" });
            DropIndex("product.Products", new[] { "ManufacturerId" });
            DropIndex("product.Products", new[] { "ArtworkId" });
            DropIndex("product.Products", new[] { "ApprDocsTypeId" });
            DropIndex("product.Products", new[] { "CountryId" });
            DropIndex("info.ProductNames", new[] { "CountryId" });
            DropIndex("info.ProductCodes", new[] { "CountryId" });
            DropIndex("info.PackSizes", new[] { "CountryId" });
            DropIndex("info.MarketingAuthorizNumbers", new[] { "CountryId" });
            DropTable("info.Strengths");
            DropTable("product.Products");
            DropTable("info.ProductNames");
            DropTable("info.ProductCodes");
            DropTable("info.PharmaceuticalForms");
            DropTable("info.PackSizes");
            DropTable("info.MarketingAuthorizNumbers");
            DropTable("info.MarketingAuthorizHolders");
            DropTable("info.Manufacturers");
            DropTable("info.Countries");
            DropTable("info.Artworks");
            DropTable("info.ApprDocsTypes");
        }
    }
}
