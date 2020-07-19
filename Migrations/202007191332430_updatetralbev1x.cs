namespace FinalProjectShopLaptop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetralbev1x : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LikeSanPham");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LikeSanPham",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Song = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.Song });
            
        }
    }
}
