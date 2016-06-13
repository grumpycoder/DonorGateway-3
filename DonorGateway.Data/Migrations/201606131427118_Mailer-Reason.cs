namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailerReason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mailers", "ReasonId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mailers", "ReasonId");
        }
    }
}
