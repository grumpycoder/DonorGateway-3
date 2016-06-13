namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class templateremoveeventname : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Templates", "EventName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Templates", "EventName", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
