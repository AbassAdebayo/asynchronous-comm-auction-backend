namespace SharedKernel
{
    /// <summary>
    /// Represents a bid placed on an auction, including the bidder's information, bid amount, and the bid's status.
    /// </summary>
    public class BidPlaced
    {
        /// <summary>
        /// Gets or sets the unique identifier for the bid.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the auction associated with this bid.
        /// </summary>
        public string AuctionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the bidder who placed the bid.
        /// </summary>
        public string Bidder { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the bid was placed.
        /// </summary>
        public DateTime BidTime { get; set; }

        /// <summary>
        /// Gets or sets the amount of the bid.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the status of the bid, indicating whether it is "Pending", "Accepted", "Outbid", etc.
        /// </summary>
        public string BidStatus { get; set; }
    }
}
