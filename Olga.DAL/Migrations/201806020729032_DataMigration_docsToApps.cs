namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_docsToApps : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ArtworkProducts", newName: "ProductArtworks");
            DropForeignKey("dbo.ProductApprDocsTypes", "Product_Id", "product.Products");
            DropForeignKey("dbo.ProductApprDocsTypes", "ApprDocsType_Id", "info.ApprDocsTypes");
            DropIndex("dbo.ProductApprDocsTypes", new[] { "Product_Id" });
            DropIndex("dbo.ProductApprDocsTypes", new[] { "ApprDocsType_Id" });
            DropPrimaryKey("dbo.ProductArtworks");
            AddColumn("dbo.ProductDocuments", "ApprDocsTypeId", c => c.Int());
            AddColumn("dbo.ProductDocuments", "ArtworkId", c => c.Int());
            AddPrimaryKey("dbo.ProductArtworks", new[] { "Product_Id", "Artwork_Id" });
            CreateIndex("dbo.ProductDocuments", "ApprDocsTypeId");
            CreateIndex("dbo.ProductDocuments", "ArtworkId");
            AddForeignKey("dbo.ProductDocuments", "ApprDocsTypeId", "info.ApprDocsTypes", "Id");
            AddForeignKey("dbo.ProductDocuments", "ArtworkId", "info.Artworks", "Id");
            DropColumn("info.Artworks", "ProductId");
            DropTable("dbo.ProductApprDocsTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductApprDocsTypes",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        ApprDocsType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.ApprDocsType_Id });
            
            AddColumn("info.Artworks", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductDocuments", "ArtworkId", "info.Artworks");
            DropForeignKey("dbo.ProductDocuments", "ApprDocsTypeId", "info.ApprDocsTypes");
            DropIndex("dbo.ProductDocuments", new[] { "ArtworkId" });
            DropIndex("dbo.ProductDocuments", new[] { "ApprDocsTypeId" });
            DropPrimaryKey("dbo.ProductArtworks");
            DropColumn("dbo.ProductDocuments", "ArtworkId");
            DropColumn("dbo.ProductDocuments", "ApprDocsTypeId");
            AddPrimaryKey("dbo.ProductArtworks", new[] { "Artwork_Id", "Product_Id" });
            CreateIndex("dbo.ProductApprDocsTypes", "ApprDocsType_Id");
            CreateIndex("dbo.ProductApprDocsTypes", "Product_Id");
            AddForeignKey("dbo.ProductApprDocsTypes", "ApprDocsType_Id", "info.ApprDocsTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductApprDocsTypes", "Product_Id", "product.Products", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.ProductArtworks", newName: "ArtworkProducts");
        }
    }
}
