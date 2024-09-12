namespace SharedKernel;

/// <summary>
/// Event triggered when a payment is successfully processed for an auction invoice.
/// This event notifies the Auction Service about the successful transaction.
/// </summary>
public class PaymentProcessed
{
    /// <summary>
    /// Gets or sets the unique identifier of the payment transaction.
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the auction for which the payment was processed.
    /// </summary>
    public string AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the invoice associated with the payment.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the amount that was paid.
    /// </summary>
    public double AmountPaid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the payment was processed.
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the payment (e.g., "Success", "Failed").
    /// </summary>
    public string Status { get; set; }
}
