namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guesteventid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Guests", name: "Event_Id", newName: "EventId");
            RenameIndex(table: "dbo.Guests", name: "IX_Event_Id", newName: "IX_EventId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Guests", name: "IX_EventId", newName: "IX_Event_Id");
            RenameColumn(table: "dbo.Guests", name: "EventId", newName: "Event_Id");
        }
    }
}
