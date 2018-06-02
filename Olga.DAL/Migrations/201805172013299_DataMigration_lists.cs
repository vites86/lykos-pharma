namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_lists : DbMigration
    {
        public override void Up()
        {
            DropColumn("product.Products", "ArtworkId");
            DropColumn("product.Products", "ApprDocsTypeId");
            DropColumn("product.Products", "ManufacturerId");
        }
        
        public override void Down()
        {
            AddColumn("product.Products", "ManufacturerId", c => c.Int(nullable: false));
            AddColumn("product.Products", "ApprDocsTypeId", c => c.Int(nullable: false));
            AddColumn("product.Products", "ArtworkId", c => c.Int(nullable: false));
        }
    }
}
