namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class templatephoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "TemplatePhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Templates", "TemplatePhoto");
        }
    }
}
