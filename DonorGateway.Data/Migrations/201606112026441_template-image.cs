namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class templateimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "Image", c => c.Binary());
            AddColumn("dbo.Templates", "MimeType", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Templates", "TemplatePhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Templates", "TemplatePhoto", c => c.Binary());
            DropColumn("dbo.Templates", "MimeType");
            DropColumn("dbo.Templates", "Image");
        }
    }
}
