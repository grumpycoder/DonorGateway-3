namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventcapacity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Capacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Capacity", c => c.Int());
        }
    }
}
