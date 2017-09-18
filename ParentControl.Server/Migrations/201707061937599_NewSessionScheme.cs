namespace ParentControl.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSessionScheme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionModels", "UniqueId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionModels", "UniqueId");
        }
    }
}
