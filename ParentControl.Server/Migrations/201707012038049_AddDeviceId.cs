namespace ParentControl.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeviceId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceModels", "DeviceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceModels", "DeviceId");
        }
    }
}
