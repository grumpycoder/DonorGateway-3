namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class templateeventname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "EventName", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Templates", "EventName");
        }
    }
}
