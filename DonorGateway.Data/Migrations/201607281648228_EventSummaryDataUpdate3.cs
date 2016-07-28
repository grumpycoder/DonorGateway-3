namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventSummaryDataUpdate3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "GuestWaitingCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "GuestAttendanceCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "TicketMailedCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "TicketMailedCount", c => c.Int());
            AlterColumn("dbo.Events", "GuestAttendanceCount", c => c.Int());
            AlterColumn("dbo.Events", "GuestWaitingCount", c => c.Int());
        }
    }
}
