namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_20180521_2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductApprDocsTypes", newName: "Products_Refs_ApprDocsTypes");
            RenameColumn(table: "dbo.Products_Refs_ApprDocsTypes", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.Products_Refs_ApprDocsTypes", name: "ApprDocsType_Id", newName: "ApprDocsTypeId");
            RenameIndex(table: "dbo.Products_Refs_ApprDocsTypes", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.Products_Refs_ApprDocsTypes", name: "IX_ApprDocsType_Id", newName: "IX_ApprDocsTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products_Refs_ApprDocsTypes", name: "IX_ApprDocsTypeId", newName: "IX_ApprDocsType_Id");
            RenameIndex(table: "dbo.Products_Refs_ApprDocsTypes", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.Products_Refs_ApprDocsTypes", name: "ApprDocsTypeId", newName: "ApprDocsType_Id");
            RenameColumn(table: "dbo.Products_Refs_ApprDocsTypes", name: "ProductId", newName: "Product_Id");
            RenameTable(name: "dbo.Products_Refs_ApprDocsTypes", newName: "ProductApprDocsTypes");
        }
    }
}
