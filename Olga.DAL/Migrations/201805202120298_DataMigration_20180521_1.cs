namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_20180521_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("info.ApprDocsTypes", "Product_Id", "product.Products");
            DropIndex("info.ApprDocsTypes", new[] { "Product_Id" });
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
            
            DropColumn("info.ApprDocsTypes", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("info.ApprDocsTypes", "Product_Id", c => c.Int());
            DropForeignKey("dbo.ProductApprDocsTypes", "ApprDocsType_Id", "info.ApprDocsTypes");
            DropForeignKey("dbo.ProductApprDocsTypes", "Product_Id", "product.Products");
            DropIndex("dbo.ProductApprDocsTypes", new[] { "ApprDocsType_Id" });
            DropIndex("dbo.ProductApprDocsTypes", new[] { "Product_Id" });
            DropTable("dbo.ProductApprDocsTypes");
            CreateIndex("info.ApprDocsTypes", "Product_Id");
            AddForeignKey("info.ApprDocsTypes", "Product_Id", "product.Products", "Id");
        }
    }
}
