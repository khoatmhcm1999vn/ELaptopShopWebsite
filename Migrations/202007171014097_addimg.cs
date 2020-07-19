namespace FinalProjectShopLaptop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "ImagePath", c => c.String());
            DropColumn("dbo.SanPhams", "Image");
            DropColumn("dbo.SanPhams", "UrlImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SanPhams", "UrlImage", c => c.String());
            AddColumn("dbo.SanPhams", "Image", c => c.Binary());
            DropColumn("dbo.SanPhams", "ImagePath");
        }
    }
}
