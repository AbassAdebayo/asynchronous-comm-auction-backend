namespace SharedKernel
{
    /// <summary>
    /// Event triggered when the auction is completed and a winner is determined.
    /// This event contains all the necessary details for generating an invoice.
    /// </summary>
    public class AuctionWinnerNotified
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
        /// Initializes a new instance of the <see cref="AuctionWinnerNotified"/> class.
        /// </summary>
        /// <param name="auctionId">The unique identifier for the auction.</param>
        /// <param name="itemDetails">The auction item details.</param>
        /// <param name="highestBidder">The highest bidder's user information.</param>
        /// <param name="winningBidAmount">The winning bid amount.</param>
        /// <param name="paymentTerms">The payment terms associated with the auction.</param>
        /// <param name="auctionCompletionDate">The date and time when the auction was completed.</param>
        /// <param name="billingAddress">The billing address of the highest bidder.</param>
        /// <param name="invoiceDate">The invoice date, which is the date the invoice should be generated.</param>
        /// <param name="paymentInstructions">Any payment instructions for the bidder.</param>
        /// <param name="refundPolicy">The refund policy for the auction.</param>
        public AuctionWinnerNotified(string auctionId, AuctionItemDetails itemDetails, BidderInfo highestBidder,
            decimal? winningBidAmount, PaymentTerms paymentTerms, DateTime auctionCompletionDate,
            string billingAddress, DateTime invoiceDate, string paymentInstructions, string refundPolicy)
        {
            AuctionId = auctionId;
            ItemDetails = itemDetails;
            HighestBidder = highestBidder;
            WinningBidAmount = winningBidAmount;
            PaymentTerms = paymentTerms;
            AuctionCompletionDate = auctionCompletionDate;
            BillingAddress = billingAddress;
            InvoiceDate = invoiceDate;
            PaymentInstructions = paymentInstructions;
            RefundPolicy = refundPolicy;
        }
    }

    /// <summary>
    /// Contains details about the auctioned item.
    /// </summary>
    public class AuctionItemDetails
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AuctionItemDetails(string itemId, string name, string description)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
        }
    }

    /// <summary>
    /// Contains information about the highest bidder.
    /// </summary>
    public class BidderInfo
    {
        public string BidderId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public BidderInfo(string bidderId, string fullName, string email)
        {
            BidderId = bidderId;
            FullName = fullName;
            Email = email;
        }
    }

    /// <summary>
    /// Represents the payment terms for the auction.
    /// </summary>
    public class PaymentTerms
    {
        public string DueDate { get; set; }
        public string Currency { get; set; }

        public PaymentTerms(string dueDate, string currency)
        {
            DueDate = dueDate;
            Currency = currency;
        }
    }
}
