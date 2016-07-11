namespace DonorGateway.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guesteventaddfieldsrefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventCode", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Events", "EventName", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "InteractionId", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "MembershipYear", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "InsideSalutation", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "HouseholdSalutation1", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "HouseholdSalutation2", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "HouseholdSalutation3", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "EmailSalutation", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Address", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Address2", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Address3", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "TicketCount", c => c.Int());
            AddColumn("dbo.Guests", "ActualDate", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "ExpeectedDate", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Comment", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Response", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "ResponseType", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "SPLCComment", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Status", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "ContactMethod", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Summary", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Category", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "SubCategory", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Owner", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Guests", "MemberYear");
            DropColumn("dbo.Guests", "InsideSal");
            DropColumn("dbo.Guests", "OutsideSal");
            DropColumn("dbo.Guests", "EmailSal");
            DropColumn("dbo.Guests", "Street");
            DropColumn("dbo.Guests", "Street2");
            DropColumn("dbo.Guests", "Street3");
            DropColumn("dbo.Guests", "GuestCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "GuestCount", c => c.Int());
            AddColumn("dbo.Guests", "Street3", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Street2", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "Street", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "EmailSal", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "OutsideSal", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "InsideSal", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Guests", "MemberYear", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Guests", "Owner");
            DropColumn("dbo.Guests", "SubCategory");
            DropColumn("dbo.Guests", "Category");
            DropColumn("dbo.Guests", "Summary");
            DropColumn("dbo.Guests", "ContactMethod");
            DropColumn("dbo.Guests", "Status");
            DropColumn("dbo.Guests", "SPLCComment");
            DropColumn("dbo.Guests", "ResponseType");
            DropColumn("dbo.Guests", "Response");
            DropColumn("dbo.Guests", "Comment");
            DropColumn("dbo.Guests", "ExpeectedDate");
            DropColumn("dbo.Guests", "ActualDate");
            DropColumn("dbo.Guests", "TicketCount");
            DropColumn("dbo.Guests", "Address3");
            DropColumn("dbo.Guests", "Address2");
            DropColumn("dbo.Guests", "Address");
            DropColumn("dbo.Guests", "EmailSalutation");
            DropColumn("dbo.Guests", "HouseholdSalutation3");
            DropColumn("dbo.Guests", "HouseholdSalutation2");
            DropColumn("dbo.Guests", "HouseholdSalutation1");
            DropColumn("dbo.Guests", "InsideSalutation");
            DropColumn("dbo.Guests", "MembershipYear");
            DropColumn("dbo.Guests", "InteractionId");
            DropColumn("dbo.Events", "EventName");
            DropColumn("dbo.Events", "EventCode");
        }
    }
}
