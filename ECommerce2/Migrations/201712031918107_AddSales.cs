namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.OrderId)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.SaleDetails",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        TaxRate = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "StatusId", "dbo.Status");
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "CompanyId", "dbo.Companies");
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.Sales", new[] { "StatusId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Sales", new[] { "CompanyId" });
            DropIndex("dbo.Sales", new[] { "OrderId" });
            DropTable("dbo.SaleDetails");
            DropTable("dbo.Sales");
        }
    }
}
