namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventtemplate3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Events", "TemplateId");
            AddForeignKey("dbo.Events", "TemplateId", "dbo.Templates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "TemplateId", "dbo.Templates");
            DropIndex("dbo.Events", new[] { "TemplateId" });
        }
    }
}
