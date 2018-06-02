namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PathToDocument = c.String(maxLength: 200),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("product.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDocuments", "ProductId", "product.Products");
            DropIndex("dbo.ProductDocuments", new[] { "ProductId" });
            DropTable("dbo.ProductDocuments");
        }
    }
}
