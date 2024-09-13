namespace RoomService.Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for auction data.
/// </summary>
public class AuctionDto
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; }
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int SoldAmount { get; set; }
    public int CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ItemImageUrl { get; set; }
}