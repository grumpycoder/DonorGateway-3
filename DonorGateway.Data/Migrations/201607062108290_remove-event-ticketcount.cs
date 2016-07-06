namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeeventticketcount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "TotalTicketCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "TotalTicketCount", c => c.Int(nullable: false));
        }
    }
}
