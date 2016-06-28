namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class responsedate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guests", "ResponseDate", c => c.DateTime(storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guests", "ResponseDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
    }
}
