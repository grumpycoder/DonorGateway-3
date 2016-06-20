namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestresponse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "ResponseDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AddColumn("dbo.Guests", "HasResponded", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "HasResponded");
            DropColumn("dbo.Guests", "ResponseDate");
        }
    }
}
