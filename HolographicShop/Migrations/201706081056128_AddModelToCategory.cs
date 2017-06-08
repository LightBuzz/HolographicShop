namespace HolographicShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ModelURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ModelURL");
        }
    }
}
