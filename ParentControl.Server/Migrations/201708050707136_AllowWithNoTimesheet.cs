namespace ParentControl.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowWithNoTimesheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleModels", "AllowWithNoTimesheet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleModels", "AllowWithNoTimesheet");
        }
    }
}
