namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smalldatetimechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Campaigns", "StartDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Campaigns", "EndDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Campaigns", "CreatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Campaigns", "UpdatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Constituents", "CreatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Constituents", "UpdatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.TaxItems", "CreatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.TaxItems", "UpdatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Events", "StartDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Events", "EndDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Events", "VenueOpenDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Events", "RegistrationCloseDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Events", "CreatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Events", "UpdatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Guests", "CreatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Guests", "UpdatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Templates", "CreatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.Templates", "UpdatedDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("Security.Users", "LockoutEndDateUtc", c => c.DateTime(storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("Security.Users", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Templates", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Templates", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Guests", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Guests", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "RegistrationCloseDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "VenueOpenDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "EndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Events", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TaxItems", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TaxItems", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Constituents", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Constituents", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Campaigns", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Campaigns", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Campaigns", "EndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Campaigns", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
    }
}
