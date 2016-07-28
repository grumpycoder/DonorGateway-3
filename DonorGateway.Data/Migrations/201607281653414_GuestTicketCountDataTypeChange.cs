namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestTicketCountDataTypeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guests", "TicketCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guests", "TicketCount", c => c.Int());
        }
    }
}
