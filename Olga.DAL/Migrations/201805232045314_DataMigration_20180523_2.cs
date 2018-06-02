namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_20180523_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("product.Products", "Artwork_Id", "info.Artworks");
            DropForeignKey("product.Products", "Manufacturer_Id", "info.Manufacturers");
            DropIndex("product.Products", new[] { "Artwork_Id" });
            DropIndex("product.Products", new[] { "Manufacturer_Id" });
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
            
            DropColumn("product.Products", "Artwork_Id");
            DropColumn("product.Products", "Manufacturer_Id");
        }
        
        public override void Down()
        {
            AddColumn("product.Products", "Manufacturer_Id", c => c.Int());
            AddColumn("product.Products", "Artwork_Id", c => c.Int());
            DropForeignKey("dbo.ManufacturerProducts", "Product_Id", "product.Products");
            DropForeignKey("dbo.ManufacturerProducts", "Manufacturer_Id", "info.Manufacturers");
            DropForeignKey("dbo.ArtworkProducts", "Product_Id", "product.Products");
            DropForeignKey("dbo.ArtworkProducts", "Artwork_Id", "info.Artworks");
            DropIndex("dbo.ManufacturerProducts", new[] { "Product_Id" });
            DropIndex("dbo.ManufacturerProducts", new[] { "Manufacturer_Id" });
            DropIndex("dbo.ArtworkProducts", new[] { "Product_Id" });
            DropIndex("dbo.ArtworkProducts", new[] { "Artwork_Id" });
            DropTable("dbo.ManufacturerProducts");
            DropTable("dbo.ArtworkProducts");
            CreateIndex("product.Products", "Manufacturer_Id");
            CreateIndex("product.Products", "Artwork_Id");
            AddForeignKey("product.Products", "Manufacturer_Id", "info.Manufacturers", "Id");
            AddForeignKey("product.Products", "Artwork_Id", "info.Artworks", "Id");
        }
    }
}
