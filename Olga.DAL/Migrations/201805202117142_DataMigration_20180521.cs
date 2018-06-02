namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_20180521 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductApprDocsTypes", "Product_Id", "product.Products");
            DropForeignKey("dbo.ProductApprDocsTypes", "ApprDocsType_Id", "info.ApprDocsTypes");
            DropIndex("dbo.ProductApprDocsTypes", new[] { "Product_Id" });
            DropIndex("dbo.ProductApprDocsTypes", new[] { "ApprDocsType_Id" });
            AddColumn("info.ApprDocsTypes", "Product_Id", c => c.Int());
            CreateIndex("info.ApprDocsTypes", "Product_Id");
            AddForeignKey("info.ApprDocsTypes", "Product_Id", "product.Products", "Id");
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
            
            DropForeignKey("info.ApprDocsTypes", "Product_Id", "product.Products");
            DropIndex("info.ApprDocsTypes", new[] { "Product_Id" });
            DropColumn("info.ApprDocsTypes", "Product_Id");
            CreateIndex("dbo.ProductApprDocsTypes", "ApprDocsType_Id");
            CreateIndex("dbo.ProductApprDocsTypes", "Product_Id");
            AddForeignKey("dbo.ProductApprDocsTypes", "ApprDocsType_Id", "info.ApprDocsTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductApprDocsTypes", "Product_Id", "product.Products", "Id", cascadeDelete: true);
        }
    }
}
