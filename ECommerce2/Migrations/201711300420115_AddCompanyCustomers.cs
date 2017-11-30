namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Customers", new[] { "CompanyId" });
            CreateTable(
                "dbo.CompanyCustomers",
                c => new
                    {
                        CompanyCustomerId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyCustomerId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerId);
            
            DropColumn("dbo.Customers", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CompanyId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CompanyCustomers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CompanyCustomers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.CompanyCustomers", new[] { "CustomerId" });
            DropIndex("dbo.CompanyCustomers", new[] { "CompanyId" });
            DropTable("dbo.CompanyCustomers");
            CreateIndex("dbo.Customers", "CompanyId");
            AddForeignKey("dbo.Customers", "CompanyId", "dbo.Companies", "CompanyId");
        }
    }
}
