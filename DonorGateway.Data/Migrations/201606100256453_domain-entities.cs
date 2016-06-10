namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class domainentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Constituents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        LookupId = c.String(maxLength: 8000, unicode: false),
                        FinderNumber = c.String(maxLength: 8000, unicode: false),
                        Street = c.String(maxLength: 8000, unicode: false),
                        Street2 = c.String(maxLength: 8000, unicode: false),
                        City = c.String(maxLength: 8000, unicode: false),
                        State = c.String(maxLength: 8000, unicode: false),
                        Zipcode = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                        Phone = c.String(maxLength: 8000, unicode: false),
                        IsUpdated = c.Boolean(),
                        UpdateStatus = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaxItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConstituentId = c.Int(nullable: false),
                        TaxYear = c.Int(nullable: false),
                        DonationDate = c.DateTime(storeType: "date"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsUpdated = c.Boolean(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Constituents", t => t.ConstituentId, cascadeDelete: true)
                .Index(t => t.ConstituentId);
            
            CreateTable(
                "dbo.Mailers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 8000, unicode: false),
                        MiddleName = c.String(maxLength: 8000, unicode: false),
                        LastName = c.String(maxLength: 8000, unicode: false),
                        Suffix = c.String(maxLength: 8000, unicode: false),
                        Address = c.String(maxLength: 8000, unicode: false),
                        Address2 = c.String(maxLength: 8000, unicode: false),
                        Address3 = c.String(maxLength: 8000, unicode: false),
                        City = c.String(maxLength: 8000, unicode: false),
                        State = c.String(maxLength: 8000, unicode: false),
                        ZipCode = c.String(maxLength: 8000, unicode: false),
                        SourceCode = c.String(maxLength: 8000, unicode: false),
                        FinderNumber = c.String(maxLength: 8000, unicode: false),
                        Suppress = c.Boolean(),
                        CampaignId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId)
                .Index(t => t.CampaignId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        ShortName = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        HeaderImage = c.Binary(),
                        HeaderText = c.String(maxLength: 8000, unicode: false),
                        BodyText = c.String(maxLength: 8000, unicode: false),
                        FooterText = c.String(maxLength: 8000, unicode: false),
                        FAQText = c.String(maxLength: 8000, unicode: false),
                        YesText = c.String(maxLength: 8000, unicode: false),
                        NoText = c.String(maxLength: 8000, unicode: false),
                        WaitText = c.String(maxLength: 8000, unicode: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mailers", "CampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.TaxItems", "ConstituentId", "dbo.Constituents");
            DropIndex("dbo.Mailers", new[] { "CampaignId" });
            DropIndex("dbo.TaxItems", new[] { "ConstituentId" });
            DropTable("dbo.Templates");
            DropTable("dbo.States");
            DropTable("dbo.Mailers");
            DropTable("dbo.TaxItems");
            DropTable("dbo.Constituents");
            DropTable("dbo.Campaigns");
        }
    }
}
