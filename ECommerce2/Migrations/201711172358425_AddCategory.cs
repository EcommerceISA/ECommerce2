namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "City_CompanyId_Description_Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Categories", "City_CompanyId_Description_Index");
            DropTable("dbo.Categories");
        }
    }
}
