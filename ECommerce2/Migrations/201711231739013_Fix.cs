namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetailTmps", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetailTmps", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
