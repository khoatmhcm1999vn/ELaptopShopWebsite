namespace FinalProjectShopLaptop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableFavor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavouriteSanPham",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProductId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteSanPham", "ProductId", "dbo.SanPhams");
            DropForeignKey("dbo.FavouriteSanPham", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FavouriteSanPham", new[] { "UserId" });
            DropIndex("dbo.FavouriteSanPham", new[] { "ProductId" });
            DropTable("dbo.FavouriteSanPham");
        }
    }
}
