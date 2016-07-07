﻿using DonorGateway.Domain;
using System;
using System.Collections.Generic;

namespace web.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speaker { get; set; }
        public string Venue { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public int Capacity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VenueOpenDate { get; set; }
        public DateTime? RegistrationCloseDate { get; set; }
        public int? TicketsAllowance { get; set; }
        public bool? IsCancelled { get; set; }
        public int RegisteredGuestCount { get; set; }
        public int WaitingGuestCount { get; set; }
        public int TicketMailedCount { get; set; }
        public int TicketMailedQueueCount { get; set; }
        public int TicketRemainingCount { get; set; }

        public Template Template { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}