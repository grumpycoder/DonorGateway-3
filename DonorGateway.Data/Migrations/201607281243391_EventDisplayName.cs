namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventDisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "DisplayName", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Events", "EventName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventName", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Events", "DisplayName");
        }
    }
}
