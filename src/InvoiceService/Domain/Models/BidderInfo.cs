namespace InvoiceService.Domain.Models
{
    public class BidderInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BidderId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

}
