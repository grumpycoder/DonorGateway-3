namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        StartDate = c.DateTime(storeType: "smalldatetime"),
                        EndDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(storeType: "smalldatetime"),
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
                        CreatedDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(storeType: "smalldatetime"),
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
                        CreatedDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(storeType: "smalldatetime"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Constituents", t => t.ConstituentId, cascadeDelete: true)
                .Index(t => t.ConstituentId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        Speaker = c.String(maxLength: 8000, unicode: false),
                        Venue = c.String(maxLength: 8000, unicode: false),
                        Street = c.String(maxLength: 8000, unicode: false),
                        City = c.String(maxLength: 8000, unicode: false),
                        State = c.String(maxLength: 8000, unicode: false),
                        Zipcode = c.String(maxLength: 8000, unicode: false),
                        Capacity = c.Int(),
                        StartDate = c.DateTime(storeType: "smalldatetime"),
                        EndDate = c.DateTime(storeType: "smalldatetime"),
                        VenueOpenDate = c.DateTime(storeType: "smalldatetime"),
                        RegistrationCloseDate = c.DateTime(storeType: "smalldatetime"),
                        TicketsAllowance = c.Int(),
                        IsCancelled = c.Boolean(),
                        TemplateId = c.Int(),
                        CreatedDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(storeType: "smalldatetime"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .Index(t => t.TemplateId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupId = c.String(maxLength: 8000, unicode: false),
                        FinderNumber = c.String(maxLength: 8000, unicode: false),
                        ConstituentType = c.String(maxLength: 8000, unicode: false),
                        SourceCode = c.String(maxLength: 8000, unicode: false),
                        MemberYear = c.String(maxLength: 8000, unicode: false),
                        LeadershipCouncil = c.String(maxLength: 8000, unicode: false),
                        InsideSal = c.String(maxLength: 8000, unicode: false),
                        OutsideSal = c.String(maxLength: 8000, unicode: false),
                        EmailSal = c.String(maxLength: 8000, unicode: false),
                        Name = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                        Phone = c.String(maxLength: 8000, unicode: false),
                        Street = c.String(maxLength: 8000, unicode: false),
                        Street2 = c.String(maxLength: 8000, unicode: false),
                        Street3 = c.String(maxLength: 8000, unicode: false),
                        City = c.String(maxLength: 8000, unicode: false),
                        State = c.String(maxLength: 8000, unicode: false),
                        Zipcode = c.String(maxLength: 8000, unicode: false),
                        Country = c.String(maxLength: 8000, unicode: false),
                        GuestCount = c.Int(),
                        IsAttending = c.Boolean(),
                        IsWaiting = c.Boolean(),
                        IsMailed = c.Boolean(),
                        ResponseDate = c.DateTime(storeType: "smalldatetime"),
                        MailedDate = c.DateTime(storeType: "smalldatetime"),
                        WaitingDate = c.DateTime(storeType: "smalldatetime"),
                        MailedBy = c.String(maxLength: 8000, unicode: false),
                        EventId = c.Int(),
                        CreatedDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(storeType: "smalldatetime"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
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
                        CancelText = c.String(maxLength: 8000, unicode: false),
                        ExpiredText = c.String(maxLength: 8000, unicode: false),
                        Image = c.Binary(),
                        MimeType = c.String(maxLength: 8000, unicode: false),
                        CreatedDate = c.DateTime(storeType: "smalldatetime"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(storeType: "smalldatetime"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ReasonId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId)
                .ForeignKey("dbo.SuppressReasons", t => t.ReasonId)
                .Index(t => t.CampaignId)
                .Index(t => t.ReasonId);
            
            CreateTable(
                "dbo.SuppressReasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Security.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, unicode: false),
                        Name = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Security.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                        RoleId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Security.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "Security.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, unicode: false),
                        FullName = c.String(maxLength: 8000, unicode: false),
                        UserPhoto = c.Binary(),
                        UserPhotoType = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 8000, unicode: false),
                        SecurityStamp = c.String(maxLength: 8000, unicode: false),
                        PhoneNumber = c.String(maxLength: 8000, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(storeType: "smalldatetime"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Security.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                        ClaimType = c.String(maxLength: 8000, unicode: false),
                        ClaimValue = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.UserRoles", "UserId", "Security.Users");
            DropForeignKey("Security.UserLogins", "UserId", "Security.Users");
            DropForeignKey("Security.UserClaims", "UserId", "Security.Users");
            DropForeignKey("Security.UserRoles", "RoleId", "Security.Roles");
            DropForeignKey("dbo.Mailers", "ReasonId", "dbo.SuppressReasons");
            DropForeignKey("dbo.Mailers", "CampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.Events", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Guests", "EventId", "dbo.Events");
            DropForeignKey("dbo.TaxItems", "ConstituentId", "dbo.Constituents");
            DropIndex("Security.UserLogins", new[] { "UserId" });
            DropIndex("Security.UserClaims", new[] { "UserId" });
            DropIndex("Security.Users", "UserNameIndex");
            DropIndex("Security.UserRoles", new[] { "RoleId" });
            DropIndex("Security.UserRoles", new[] { "UserId" });
            DropIndex("Security.Roles", "RoleNameIndex");
            DropIndex("dbo.Mailers", new[] { "ReasonId" });
            DropIndex("dbo.Mailers", new[] { "CampaignId" });
            DropIndex("dbo.Guests", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "TemplateId" });
            DropIndex("dbo.TaxItems", new[] { "ConstituentId" });
            DropTable("Security.UserLogins");
            DropTable("Security.UserClaims");
            DropTable("Security.Users");
            DropTable("dbo.States");
            DropTable("Security.UserRoles");
            DropTable("Security.Roles");
            DropTable("dbo.SuppressReasons");
            DropTable("dbo.Mailers");
            DropTable("dbo.Templates");
            DropTable("dbo.Guests");
            DropTable("dbo.Events");
            DropTable("dbo.TaxItems");
            DropTable("dbo.Constituents");
            DropTable("dbo.Campaigns");
        }
    }
}
