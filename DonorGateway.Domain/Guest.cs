namespace DonorGateway.Domain
{
    public class Guest : BaseEntity
    {
        public string AccountId { get; set; }
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

        public bool? IsAttending { get; set; }
        public int? GuestCount { get; set; }

        public virtual Event Event { get; set; }
    }
}