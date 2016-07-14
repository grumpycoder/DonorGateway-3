using System;

namespace rsvp.web.ViewModels
{
    public class GuestRegisterViewModel
    {
        public int Id { get; set; }
        public string PromoCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Comment { get; set; }
        public int? TicketCount { get; set; }
        public bool? IsAttending { get; set; } = false;
        public DateTime? ResponseDate { get; set; }
        public int? EventId { get; set; }
        public string EventName { get; set; }
    }
}