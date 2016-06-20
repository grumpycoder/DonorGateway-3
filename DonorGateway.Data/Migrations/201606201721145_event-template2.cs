namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventtemplate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "TemplateId", "dbo.Templates");
            DropIndex("dbo.Events", new[] { "TemplateId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Events", "TemplateId");
            AddForeignKey("dbo.Events", "TemplateId", "dbo.Templates", "Id");
        }
    }
}
