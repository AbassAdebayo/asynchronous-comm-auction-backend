using System.ComponentModel.DataAnnotations.Schema;

namespace RoomService.Domain.Models
{

    /// <summary>
    /// Entity class representing an auction.
    /// </summary>
    public class Auction
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionEnd { get; set; }
        public AuctionStatus Status { get; set; }
        public Item Item { get; set; }

        public bool HasReservePrice() => ReservePrice > 0;

        public void UpdateDetails(string make, string model, int? year, string color, int? mileage)
        {
            if (Item != null)
            {
                Item.Make = make;
                Item.Model = model;
                Item.Year = year ?? Item.Year;
                Item.Color = color;
                Item.Mileage = mileage ?? Item.Mileage;
            }
        }
    }
}
