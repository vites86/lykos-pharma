namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_20180521_3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products_Refs_ApprDocsTypes", newName: "ProductApprDocsTypes");
            RenameColumn(table: "dbo.ProductApprDocsTypes", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.ProductApprDocsTypes", name: "ApprDocsTypeId", newName: "ApprDocsType_Id");
            RenameIndex(table: "dbo.ProductApprDocsTypes", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.ProductApprDocsTypes", name: "IX_ApprDocsTypeId", newName: "IX_ApprDocsType_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductApprDocsTypes", name: "IX_ApprDocsType_Id", newName: "IX_ApprDocsTypeId");
            RenameIndex(table: "dbo.ProductApprDocsTypes", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameColumn(table: "dbo.ProductApprDocsTypes", name: "ApprDocsType_Id", newName: "ApprDocsTypeId");
            RenameColumn(table: "dbo.ProductApprDocsTypes", name: "Product_Id", newName: "ProductId");
            RenameTable(name: "dbo.ProductApprDocsTypes", newName: "Products_Refs_ApprDocsTypes");
        }
    }
}
