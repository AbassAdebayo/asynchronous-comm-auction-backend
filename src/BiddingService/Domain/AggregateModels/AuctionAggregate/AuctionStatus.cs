namespace BiddingService.Domain.AggregateModels.AuctionAggregate;

/// <summary>
/// Enum representing the status of an auction.
/// </summary>
public enum AuctionStatus
{
    Pending,
    Active,
    Finished,
    ReserveNotMet,
    PaymentPending,
    Paid,
    Failed,
    Disputed
}