namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        Logo = c.String(),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.Name, unique: true, name: "Company_Name_Index")
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
            AddForeignKey("dbo.Cities", "StateId", "dbo.States", "StateId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropForeignKey("dbo.Companies", "StateId", "dbo.States");
            DropForeignKey("dbo.Companies", "CityId", "dbo.Cities");
            DropIndex("dbo.Companies", new[] { "CityId" });
            DropIndex("dbo.Companies", new[] { "StateId" });
            DropIndex("dbo.Companies", "Company_Name_Index");
            DropTable("dbo.Companies");
            AddForeignKey("dbo.Cities", "StateId", "dbo.States", "StateId", cascadeDelete: true);
        }
    }
}
