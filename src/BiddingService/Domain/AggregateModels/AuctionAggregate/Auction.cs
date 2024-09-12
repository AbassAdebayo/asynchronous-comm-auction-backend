using MongoDB.Entities;

namespace BiddingService.Domain.AggregateModels.AuctionAggregate;

/// <summary>
/// Represents an auction with its properties, inheriting from MongoDB.Entities.Entity for database mapping.
/// </summary>
public class Auction : Entity
{
    public DateTime AuctionEnd { get; set; }
    public string Seller { get; set; }
    public int ReservePrice { get; set; }
    public AuctionStatus Status { get; set; }
}