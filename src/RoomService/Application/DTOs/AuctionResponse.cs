namespace RoomService.Application.DTOs;

public record AuctionResponse(
    Guid Id,
    int ReservePrice,
    string Seller,
    string Winner,
    int SoldAmount,
    int CurrentHighBid,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime AuctionEnd,
    string Status,
    string Name,
    string Description,
    string ItemImageUrl);
