namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        TaxRate = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropTable("dbo.OrderDetails");
        }
    }
}
