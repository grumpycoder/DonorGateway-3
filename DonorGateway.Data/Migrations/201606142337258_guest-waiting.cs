namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestwaiting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "IsWating", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "IsWating");
        }
    }
}
