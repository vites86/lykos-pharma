namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_collections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("product.Products", "ApprDocsTypeId", "info.ApprDocsTypes");
            DropForeignKey("product.Products", "ArtworkId", "info.Artworks");
            DropForeignKey("product.Products", "ManufacturerId", "info.Manufacturers");
            DropIndex("product.Products", new[] { "ApprDocsTypeId" });
            DropIndex("product.Products", new[] { "ArtworkId" });
            DropIndex("product.Products", new[] { "ManufacturerId" });
            CreateTable(
                "dbo.ProductApprDocsTypes",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        ApprDocsType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.ApprDocsType_Id })
                .ForeignKey("product.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("info.ApprDocsTypes", t => t.ApprDocsType_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.ApprDocsType_Id);
            
            CreateTable(
                "dbo.ArtworkProducts",
                c => new
                    {
                        Artwork_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artwork_Id, t.Product_Id })
                .ForeignKey("info.Artworks", t => t.Artwork_Id, cascadeDelete: true)
                .ForeignKey("product.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Artwork_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ManufacturerProducts",
                c => new
                    {
                        Manufacturer_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manufacturer_Id, t.Product_Id })
                .ForeignKey("info.Manufacturers", t => t.Manufacturer_Id, cascadeDelete: true)
                .ForeignKey("product.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Manufacturer_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("info.ApprDocsTypes", "ProductId", c => c.Int(nullable: false));
            AddColumn("info.Artworks", "ProductId", c => c.Int(nullable: false));
            AddColumn("info.Manufacturers", "ProductId", c => c.Int(nullable: false));
            AlterColumn("product.Products", "ApprDocsTypeId", c => c.Int(nullable: false));
            AlterColumn("product.Products", "ArtworkId", c => c.Int(nullable: false));
            AlterColumn("product.Products", "ManufacturerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManufacturerProducts", "Product_Id", "product.Products");
            DropForeignKey("dbo.ManufacturerProducts", "Manufacturer_Id", "info.Manufacturers");
            DropForeignKey("dbo.ArtworkProducts", "Product_Id", "product.Products");
            DropForeignKey("dbo.ArtworkProducts", "Artwork_Id", "info.Artworks");
            DropForeignKey("dbo.ProductApprDocsTypes", "ApprDocsType_Id", "info.ApprDocsTypes");
            DropForeignKey("dbo.ProductApprDocsTypes", "Product_Id", "product.Products");
            DropIndex("dbo.ManufacturerProducts", new[] { "Product_Id" });
            DropIndex("dbo.ManufacturerProducts", new[] { "Manufacturer_Id" });
            DropIndex("dbo.ArtworkProducts", new[] { "Product_Id" });
            DropIndex("dbo.ArtworkProducts", new[] { "Artwork_Id" });
            DropIndex("dbo.ProductApprDocsTypes", new[] { "ApprDocsType_Id" });
            DropIndex("dbo.ProductApprDocsTypes", new[] { "Product_Id" });
            AlterColumn("product.Products", "ManufacturerId", c => c.Int());
            AlterColumn("product.Products", "ArtworkId", c => c.Int());
            AlterColumn("product.Products", "ApprDocsTypeId", c => c.Int());
            DropColumn("info.Manufacturers", "ProductId");
            DropColumn("info.Artworks", "ProductId");
            DropColumn("info.ApprDocsTypes", "ProductId");
            DropTable("dbo.ManufacturerProducts");
            DropTable("dbo.ArtworkProducts");
            DropTable("dbo.ProductApprDocsTypes");
            CreateIndex("product.Products", "ManufacturerId");
            CreateIndex("product.Products", "ArtworkId");
            CreateIndex("product.Products", "ApprDocsTypeId");
            AddForeignKey("product.Products", "ManufacturerId", "info.Manufacturers", "Id");
            AddForeignKey("product.Products", "ArtworkId", "info.Artworks", "Id");
            AddForeignKey("product.Products", "ApprDocsTypeId", "info.ApprDocsTypes", "Id");
        }
    }
}
