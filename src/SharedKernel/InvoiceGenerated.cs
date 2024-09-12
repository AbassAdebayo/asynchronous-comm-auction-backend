namespace SharedKernel;

/// <summary>
/// Event triggered when an invoice is generated for the highest bidder in an auction.
/// This event carries all the necessary details for the Payment Service to process the payment.
/// </summary>
public class InvoiceGenerated
{
    /// <summary>
    /// Gets or sets the unique identifier for the invoice.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the auction for which the invoice is generated.
    /// </summary>
    public string AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the details of the auction item.
    /// </summary>
    public AuctionItemDetails ItemDetails { get; set; }

    /// <summary>
    /// Gets or sets the information about the highest bidder.
    /// </summary>
    public BidderInfo HighestBidder { get; set; }

    /// <summary>
    /// Gets or sets the amount of the winning bid.
    /// </summary>
    public double WinningBidAmount { get; set; }

    /// <summary>
    /// Gets or sets the payment terms, including taxes and fees.
    /// </summary>
    public PaymentTerms PaymentTerms { get; set; }

    /// <summary>
    /// Gets or sets the date when the invoice was generated.
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Gets or sets the billing address of the highest bidder.
    /// </summary>
    public string BillingAddress { get; set; }

    /// <summary>
    /// Gets or sets the payment instructions that will be provided to the highest bidder.
    /// </summary>
    public string PaymentInstructions { get; set; }

    /// <summary>
    /// Gets or sets the refund policy associated with the auction.
    /// </summary>
    public string RefundPolicy { get; set; }

    /// <summary>
    /// Gets or sets the total taxes and fees associated with the invoice.
    /// </summary>
    public decimal TaxesAndFees { get; set; }
}