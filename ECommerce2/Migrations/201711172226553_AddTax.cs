namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTax : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Taxes",
                c => new
                    {
                        TaxId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        Rate = c.Double(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaxId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Tax_CompanyId_Description_Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Taxes", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Taxes", "Tax_CompanyId_Description_Index");
            DropTable("dbo.Taxes");
        }
    }
}
