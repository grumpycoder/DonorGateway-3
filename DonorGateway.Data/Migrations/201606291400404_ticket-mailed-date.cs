namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketmaileddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "TicketMailDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.Guests", "TicketMailed", c => c.Boolean());
            DropColumn("dbo.Guests", "TicketIssued");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "TicketIssued", c => c.Boolean());
            DropColumn("dbo.Guests", "TicketMailed");
            DropColumn("dbo.Guests", "TicketMailDate");
        }
    }
}
