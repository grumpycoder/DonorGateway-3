namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventticketcount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TotalTicketCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "TotalTicketCount");
        }
    }
}
