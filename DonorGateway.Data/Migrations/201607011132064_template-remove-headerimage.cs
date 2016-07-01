namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class templateremoveheaderimage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Templates", "HeaderImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Templates", "HeaderImage", c => c.Binary());
        }
    }
}
