namespace BiddingService.Application.Models
{
    public record BidResponse(
        string Id,
        string AuctionId,
        string Bidder,
        DateTime BidTime,
        int Amount,
        string BidStatus
    );
}
