namespace InvoiceService.Domain.AggregateModels
{
    public class AuctionItemDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
