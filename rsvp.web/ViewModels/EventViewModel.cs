using DonorGateway.Domain;

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

        public string PromoCode { get; set; }

        public int? TemplateId { get; set; }
        public int EventId { get; set; }


    }
}