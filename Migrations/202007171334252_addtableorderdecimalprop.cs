namespace FinalProjectShopLaptop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableorderdecimalprop : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SanPhams", "Price", c => c.Decimal(precision: 18, scale: 0));
            AlterColumn("dbo.OrderDetail", "Price", c => c.Decimal(precision: 18, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetail", "Price", c => c.Single());
            AlterColumn("dbo.SanPhams", "Price", c => c.Single(nullable: false));
        }
    }
}
