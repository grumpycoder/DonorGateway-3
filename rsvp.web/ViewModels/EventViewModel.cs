using DonorGateway.Domain;
using System;

namespace rsvp.web.ViewModels
{
    public class EventViewModel
    {
        public string PromoCode { get; set; }

        public string Name { get; set; }
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
        public bool IsCancelled { get; set; }

        public int? TemplateId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }

        public bool IsExpired => EndDate < DateTime.Now;
        public bool IsAtCapacity => TicketRemainingCount <= 0;
        public int RegisteredGuestCount { get; set; }
        public int WaitingGuestCount { get; set; }
        public int TicketRemainingCount => Capacity - RegisteredGuestCount;

        public Template Template { get; set; }
    }
}

