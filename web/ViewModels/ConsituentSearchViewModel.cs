using DonorGateway.Domain;

namespace web.ViewModels
{
    public class ConsituentSearchViewModel : Pager<Constituent>
    {
        public string Name { get; set; }
        public string FinderNumber { get; set; }
        public string LookupId { get; set; }
        public string Zipcode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UpdateStatus? UpdateStatus { get; set; }
    }
}