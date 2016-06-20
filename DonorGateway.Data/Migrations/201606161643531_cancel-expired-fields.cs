namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancelexpiredfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IsCancelled", c => c.Boolean());
            AddColumn("dbo.Templates", "CancelText", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Templates", "ExpiredText", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Templates", "ExpiredText");
            DropColumn("dbo.Templates", "CancelText");
            DropColumn("dbo.Events", "IsCancelled");
        }
    }
}
