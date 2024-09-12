namespace SharedKernel
{
    /// <summary>
    /// Represents the completion state of an auction, indicating whether an item was sold, to whom, and for what amount.
    /// </summary>
    public class AuctionFinished
    {
        /// <summary>
        /// Gets or sets the unique identifier for the auction.
        /// </summary>
        public string AuctionId { get; set; }

        /// <summary>
        /// Gets or sets the auction item details.
        /// </summary>
        public AuctionItemDetails ItemDetails { get; set; }

        /// <summary>
        /// Gets or sets the highest bidder's user information.
        /// </summary>
        public BidderInfo HighestBidder { get; set; }

        /// <summary>
        /// Gets or sets the winning bid amount.
        /// </summary>
        public decimal? WinningBidAmount { get; set; }

        /// <summary>
        /// Gets or sets the payment terms associated with the auction.
        /// </summary>
        public PaymentTerms PaymentTerms { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the auction was completed.
        /// </summary>
        public DateTime AuctionCompletionDate { get; set; }

        /// <summary>
        /// Gets or sets the billing address of the highest bidder.
        /// </summary>
        public string BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the invoice date, which is the date the invoice should be generated.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets any payment instructions for the bidder.
        /// </summary>
        public string PaymentInstructions { get; set; }

        /// <summary>
        /// Gets or sets the refund policy for the auction, in case the payment or transaction fails.
        /// </summary>
        public string RefundPolicy { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the item was sold in the auction.
        /// </summary>
        public bool ItemSold { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the winning bidder.
        /// </summary>
        public string Winner { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the seller.
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// Gets or sets the amount for which the item was sold, if applicable.
        /// This value is null if the item was not sold.
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
