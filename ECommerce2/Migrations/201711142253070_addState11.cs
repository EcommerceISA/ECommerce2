namespace ECommerce2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addState11 : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.States", name: "Department_Name_Index", newName: "State_Name_Index");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.States", name: "State_Name_Index", newName: "Department_Name_Index");
        }
    }
}
