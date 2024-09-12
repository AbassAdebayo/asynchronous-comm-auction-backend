namespace InvoiceService.Domain.AggregateModels
{
    public class PaymentTerms
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DueDate { get; set; }
        public string Currency { get; set; }
    }
}
