namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestleadcouncilbool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guests", "LeadershipCouncil", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guests", "LeadershipCouncil", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
