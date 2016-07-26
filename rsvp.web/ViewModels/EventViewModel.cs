using DonorGateway.Domain;
using System;

namespace rsvp.web.ViewModels
{
    public class EventViewModel
    {
        public string Name { get; set; }
        public Template Template { get; set; }
        public string Speaker { get; set; }
        public string Venue { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VenueOpenDate { get; set; }
        public int Capacity { get; set; }

        public int? TicketAllowance { get; set; }

        public string PromoCode { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsExpired => EndDate < DateTime.Now;

        public int? TemplateId { get; set; }
        public int EventId { get; set; }

        public int RegisteredGuestCount { get; set; }
        public int WaitingGuestCount { get; set; }
        public int TicketMailedCount { get; set; }
        public int TicketMailedQueueCount { get; set; }
        public int TicketRemainingCount { get; set; }
    }
}

