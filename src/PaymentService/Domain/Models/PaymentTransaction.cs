namespace PaymentService.Domain.Models;

/// <summary>
/// Represents a payment transaction record.
/// </summary>
public class PaymentTransaction
{
    /// <summary>
    /// Gets or sets the unique identifier for the payment transaction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated auction.
    /// </summary>
    public Guid AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated invoice.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the amount paid in the transaction.
    /// </summary>
    public double AmountPaid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the payment was made.
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the payment transaction (e.g., "Success", "Failed").
    /// </summary>
    public string? Status { get; set; }
}
