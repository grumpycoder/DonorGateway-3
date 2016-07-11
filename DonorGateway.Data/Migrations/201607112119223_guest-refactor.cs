namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestrefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "StateName", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Guests", "Response");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "Response", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Guests", "StateName");
        }
    }
}
