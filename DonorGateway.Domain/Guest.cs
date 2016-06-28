using System;

namespace DonorGateway.Domain
{
    public class Guest : BaseEntity
    {
        public string AccountId { get; set; }
        public string FinderNumber { get; set; }
        public string GuestType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public DateTime? ResponseDate { get; set; }

        public bool? IsAttending { get; set; } = false;
        public bool? IsWaiting { get; set; } = false;
        public bool? TicketIssued { get; set; } = false;
        public bool? HasResponded { get; set; } = false;
        public int? GuestCount { get; set; }
        public int? EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}