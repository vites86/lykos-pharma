namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_20180520 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtworkProducts", "Artwork_Id", "info.Artworks");
            DropForeignKey("dbo.ArtworkProducts", "Product_Id", "product.Products");
            DropForeignKey("dbo.ManufacturerProducts", "Manufacturer_Id", "info.Manufacturers");
            DropForeignKey("dbo.ManufacturerProducts", "Product_Id", "product.Products");
            DropIndex("dbo.ArtworkProducts", new[] { "Artwork_Id" });
            DropIndex("dbo.ArtworkProducts", new[] { "Product_Id" });
            DropIndex("dbo.ManufacturerProducts", new[] { "Manufacturer_Id" });
            DropIndex("dbo.ManufacturerProducts", new[] { "Product_Id" });
            AddColumn("product.Products", "Artwork_Id", c => c.Int());
            AddColumn("product.Products", "Manufacturer_Id", c => c.Int());
            CreateIndex("product.Products", "Artwork_Id");
            CreateIndex("product.Products", "Manufacturer_Id");
            AddForeignKey("product.Products", "Artwork_Id", "info.Artworks", "Id");
            AddForeignKey("product.Products", "Manufacturer_Id", "info.Manufacturers", "Id");
            DropColumn("info.ApprDocsTypes", "ProductId");
            DropTable("dbo.ArtworkProducts");
            DropTable("dbo.ManufacturerProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ManufacturerProducts",
                c => new
                    {
                        Manufacturer_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manufacturer_Id, t.Product_Id });
            
            CreateTable(
                "dbo.ArtworkProducts",
                c => new
                    {
                        Artwork_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artwork_Id, t.Product_Id });
            
            AddColumn("info.ApprDocsTypes", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("product.Products", "Manufacturer_Id", "info.Manufacturers");
            DropForeignKey("product.Products", "Artwork_Id", "info.Artworks");
            DropIndex("product.Products", new[] { "Manufacturer_Id" });
            DropIndex("product.Products", new[] { "Artwork_Id" });
            DropColumn("product.Products", "Manufacturer_Id");
            DropColumn("product.Products", "Artwork_Id");
            CreateIndex("dbo.ManufacturerProducts", "Product_Id");
            CreateIndex("dbo.ManufacturerProducts", "Manufacturer_Id");
            CreateIndex("dbo.ArtworkProducts", "Product_Id");
            CreateIndex("dbo.ArtworkProducts", "Artwork_Id");
            AddForeignKey("dbo.ManufacturerProducts", "Product_Id", "product.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ManufacturerProducts", "Manufacturer_Id", "info.Manufacturers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArtworkProducts", "Product_Id", "product.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArtworkProducts", "Artwork_Id", "info.Artworks", "Id", cascadeDelete: true);
        }
    }
}
