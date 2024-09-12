namespace SharedKernel
{
    /// <summary>
    /// Represents the details of an auction when it is created.
    /// This class is used to transfer auction data within the system or between systems.
    /// </summary>
    public class AuctionCreated
    {
        /// <summary>
        /// Gets or sets the unique identifier for the auction.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the reserve price for the auction.
        /// This is the minimum price at which the seller is willing to sell the item.
        /// </summary>
        public int ReservePrice { get; set; }

        /// <summary>
        /// Gets or sets the seller's identifier.
        /// </summary>
        public string Seller { get; set; } = default!;

        /// <summary>
        /// Gets or sets the identifier of the winning bidder, if any.
        /// </summary>
        public string Winner { get; set; } = default!;

        /// <summary>
        /// Gets or sets the amount for which the item was sold.
        /// This is set only if the item is sold.
        /// </summary>
        public int SoldAmount { get; set; }

        /// <summary>
        /// Gets or sets the current highest bid amount.
        /// This represents the highest bid placed so far in the auction.
        /// </summary>
        public int CurrentHighBid { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the auction was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the auction was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the auction is scheduled to end.
        /// </summary>
        public DateTime AuctionEnd { get; set; }

        /// <summary>
        /// Gets or sets the current status of the auction.
        /// This can be "Active", "Completed", "Cancelled", etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the make of the item being auctioned, if applicable.
        /// </summary>
        public string? Make { get; set; }

        /// <summary>
        /// Gets or sets the model of the item being auctioned, if applicable.
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// Gets or sets the year of manufacture for the item being auctioned, if applicable.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the color of the item being auctioned, if applicable.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Gets or sets the mileage of the item being auctioned, if applicable.
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image representing the item being auctioned.
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}
