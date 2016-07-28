namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventSummaryDataUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "GuestAttendanceCount", c => c.Int());
            DropColumn("dbo.Events", "GuestRegisteredCount");
            DropColumn("dbo.Events", "TicketRemainingCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "TicketRemainingCount", c => c.Int());
            AddColumn("dbo.Events", "GuestRegisteredCount", c => c.Int());
            DropColumn("dbo.Events", "GuestAttendanceCount");
        }
    }
}
