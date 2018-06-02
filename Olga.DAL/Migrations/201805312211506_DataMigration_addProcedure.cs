namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_addProcedure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SubmissionDate = c.DateTime(nullable: false),
                        ApprovalDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        ProcedureType = c.Int(nullable: false),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("product.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Remarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemarkDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        Procedure_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Procedures", t => t.Procedure_Id)
                .Index(t => t.Procedure_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Remarks", "Procedure_Id", "dbo.Procedures");
            DropForeignKey("dbo.Procedures", "ProductId", "product.Products");
            DropIndex("dbo.Remarks", new[] { "Procedure_Id" });
            DropIndex("dbo.Procedures", new[] { "ProductId" });
            DropTable("dbo.Remarks");
            DropTable("dbo.Procedures");
        }
    }
}
