namespace DonorGateway.Domain
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }
        public byte[] HeaderImage { get; set; }
        public string HeaderText { get; set; }
        public string BodyText { get; set; }
        public string FooterText { get; set; }
        public string FAQText { get; set; }
        public string YesText { get; set; }
        public string NoText { get; set; }
        public string WaitText { get; set; }
        public string CancelText { get; set; }
        public string ExpiredText { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
    }
}