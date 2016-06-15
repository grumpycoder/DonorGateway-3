namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestwaitingupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "IsWaiting", c => c.Boolean());
            DropColumn("dbo.Guests", "IsWating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "IsWating", c => c.Boolean());
            DropColumn("dbo.Guests", "IsWaiting");
        }
    }
}
