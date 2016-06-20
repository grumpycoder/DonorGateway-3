namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventtemplate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TemplateId", c => c.Int());
            CreateIndex("dbo.Events", "TemplateId");
            AddForeignKey("dbo.Events", "TemplateId", "dbo.Templates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "TemplateId", "dbo.Templates");
            DropIndex("dbo.Events", new[] { "TemplateId" });
            DropColumn("dbo.Events", "TemplateId");
        }
    }
}
