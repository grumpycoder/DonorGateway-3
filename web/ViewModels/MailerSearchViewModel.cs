using DonorGateway.Domain;

namespace web.ViewModels
{
    public class MailerSearchViewModel : Pager<Mailer>
    {
        public string FinderNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int CampaignId { get; set; }
        public int? ReasonId { get; set; }
        public string SourceCode { get; set; }
        public bool? Suppress { get; set; }
    }
}