namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventrefacotr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TicketAllowance", c => c.Int());
            DropColumn("dbo.Events", "TicketsAllowance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "TicketsAllowance", c => c.Int());
            DropColumn("dbo.Events", "TicketAllowance");
        }
    }
}
