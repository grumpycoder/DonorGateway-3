namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuppressionReason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuppressReasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Mailers", "ReasonId");
            AddForeignKey("dbo.Mailers", "ReasonId", "dbo.SuppressReasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mailers", "ReasonId", "dbo.SuppressReasons");
            DropIndex("dbo.Mailers", new[] { "ReasonId" });
            DropTable("dbo.SuppressReasons");
        }
    }
}
