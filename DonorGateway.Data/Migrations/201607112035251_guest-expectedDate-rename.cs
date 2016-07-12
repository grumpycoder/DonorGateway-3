namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestexpectedDaterename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "ExpectedDate", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Guests", "ExpeectedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "ExpeectedDate", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Guests", "ExpectedDate");
        }
    }
}