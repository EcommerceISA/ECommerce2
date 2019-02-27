namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarehouse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WarehouseId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => new { t.CompanyId, t.Name }, unique: true, name: "Warehouse_CompanyId_Name_Index")
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehouses", "StateId", "dbo.States");
            DropForeignKey("dbo.Warehouses", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Warehouses", "CityId", "dbo.Cities");
            DropIndex("dbo.Warehouses", new[] { "CityId" });
            DropIndex("dbo.Warehouses", new[] { "StateId" });
            DropIndex("dbo.Warehouses", "Warehouse_CompanyId_Name_Index");
            DropTable("dbo.Warehouses");
        }
    }
}
