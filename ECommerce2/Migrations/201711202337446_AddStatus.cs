namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusId)
                .Index(t => t.Description, unique: true, name: "Status_description_Index");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Status", "Status_description_Index");
            DropTable("dbo.Status");
        }
    }
}
