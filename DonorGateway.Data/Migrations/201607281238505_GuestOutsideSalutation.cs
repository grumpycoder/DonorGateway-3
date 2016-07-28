namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestOutsideSalutation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "OutsideSalutation", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "OutsideSalutation");
        }
    }
}
