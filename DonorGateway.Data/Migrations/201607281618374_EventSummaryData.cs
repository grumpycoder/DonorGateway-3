namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventSummaryData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "GuestWaitingCount", c => c.Int());
            AddColumn("dbo.Events", "GuestRegisteredCount", c => c.Int());
            AddColumn("dbo.Events", "TicketRemainingCount", c => c.Int());
            AddColumn("dbo.Events", "TicketMailedCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "TicketMailedCount");
            DropColumn("dbo.Events", "TicketRemainingCount");
            DropColumn("dbo.Events", "GuestRegisteredCount");
            DropColumn("dbo.Events", "GuestWaitingCount");
        }
    }
}
