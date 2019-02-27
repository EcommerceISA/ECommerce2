namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderCompanyId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CompanyId");
            AddForeignKey("dbo.Orders", "CompanyId", "dbo.Companies", "CompanyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Orders", new[] { "CompanyId" });
            DropColumn("dbo.Orders", "CompanyId");
        }
    }
}
