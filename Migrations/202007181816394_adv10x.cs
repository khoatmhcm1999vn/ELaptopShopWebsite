namespace FinalProjectShopLaptop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adv10x : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FavouriteSanPham", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavouriteSanPham", "ProductId", "dbo.SanPhams");
            DropIndex("dbo.FavouriteSanPham", new[] { "ProductId" });
            DropIndex("dbo.FavouriteSanPham", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.FavouriteSanPham", "UserId");
            CreateIndex("dbo.FavouriteSanPham", "ProductId");
            AddForeignKey("dbo.FavouriteSanPham", "ProductId", "dbo.SanPhams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FavouriteSanPham", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
