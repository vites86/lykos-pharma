namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_unLimited : DbMigration
    {
        public override void Up()
        {
            AddColumn("product.Products", "UnLimited", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("product.Products", "UnLimited");
        }
    }
}
