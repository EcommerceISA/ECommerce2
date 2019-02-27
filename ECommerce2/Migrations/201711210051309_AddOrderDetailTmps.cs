namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderDetailTmps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetailTmps",
                c => new
                    {
                        OrderDetailTmpId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        TaxRate = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailTmpId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetailTmps", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderDetailTmps", new[] { "ProductId" });
            DropTable("dbo.OrderDetailTmps");
        }
    }
}
