namespace FinalProjectShopLaptop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adv10x51 : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.LikeSanPham");
        }
    }
}
