using System;

namespace DonorGateway.Domain
{
    public class Guest : BaseEntity
    {
        public string LookupId { get; set; }
        public string FinderNumber { get; set; }
        public string ConstituentType { get; set; }
        public string SourceCode { get; set; }

        public string MemberYear { get; set; }
        public bool? LeadershipCouncil { get; set; }
        public string InsideSal { get; set; }
        public string OutsideSal { get; set; }
        public string EmailSal { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        
        public int? GuestCount { get; set; }
        public bool? IsAttending { get; set; } = false;
        public bool? IsWaiting { get; set; } = false;
        public bool? IsMailed { get; set; } = false;

        public DateTime? ResponseDate { get; set; }
        public DateTime? MailedDate { get; set; }
        public DateTime? WaitingDate { get; set; }

        public string MailedBy { get; set; }
        
        public int? EventId { get; set; }

    }
}