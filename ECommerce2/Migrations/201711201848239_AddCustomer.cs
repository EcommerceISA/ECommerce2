namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.CompanyId)
                .Index(t => t.UserName, unique: true, name: "Customer_UserName_Index")
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "StateId", "dbo.States");
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Customers", "CityId", "dbo.Cities");
            DropIndex("dbo.Customers", new[] { "CityId" });
            DropIndex("dbo.Customers", new[] { "StateId" });
            DropIndex("dbo.Customers", "Customer_UserName_Index");
            DropIndex("dbo.Customers", new[] { "CompanyId" });
            DropTable("dbo.Customers");
        }
    }
}
