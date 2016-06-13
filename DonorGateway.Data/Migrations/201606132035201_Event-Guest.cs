namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventGuest : DbMigration
    {
        public override void Up()
        {
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
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        VenueOpenDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RegistrationCloseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TicketsAllowance = c.Int(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.String(maxLength: 8000, unicode: false),
                        GuestType = c.String(maxLength: 8000, unicode: false),
                        Name = c.String(maxLength: 8000, unicode: false),
                        Email = c.String(maxLength: 8000, unicode: false),
                        Phone = c.String(maxLength: 8000, unicode: false),
                        Street = c.String(maxLength: 8000, unicode: false),
                        Street2 = c.String(maxLength: 8000, unicode: false),
                        Street3 = c.String(maxLength: 8000, unicode: false),
                        City = c.String(maxLength: 8000, unicode: false),
                        State = c.String(maxLength: 8000, unicode: false),
                        Zipcode = c.String(maxLength: 8000, unicode: false),
                        IsAttending = c.Boolean(),
                        GuestCount = c.Int(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 8000, unicode: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 8000, unicode: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guests", "Event_Id", "dbo.Events");
            DropIndex("dbo.Guests", new[] { "Event_Id" });
            DropTable("dbo.Guests");
            DropTable("dbo.Events");
        }
    }
}
