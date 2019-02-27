namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                        BarCode = c.String(nullable: false, maxLength: 50),
                        CategoryId = c.Int(nullable: false),
                        TaxId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Taxes", t => t.TaxId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Product_CompanyId_Description_Index")
                .Index(t => t.BarCode, unique: true, name: "Product_CompanyId_BarCode_Index")
                .Index(t => t.CategoryId)
                .Index(t => t.TaxId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "TaxId", "dbo.Taxes");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "TaxId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", "Product_CompanyId_BarCode_Index");
            DropIndex("dbo.Products", "Product_CompanyId_Description_Index");
            DropTable("dbo.Products");
        }
    }
}
