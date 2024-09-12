using MongoDB.Entities;

namespace BiddingService.Domain.AggregateModels.BiddingAggregate;

/// <summary>
/// Represents a bid on an auction, inheriting from MongoDB.Entities.Entity for database mapping.
/// </summary>
public class Bid : Entity
{
    public string AuctionId { get; set; }
    public string Bidder { get; set; }
    public DateTime BidTime { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public BidStatus BidStatus { get; set; }
}