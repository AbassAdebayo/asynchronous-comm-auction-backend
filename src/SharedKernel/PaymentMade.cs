namespace SharedKernel
{
    /// <summary>
    /// Represents an event indicating that a payment has been made.
    /// This class is used to communicate payment information within the system and to external services.
    /// </summary>
    public class PaymentMade
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who made the payment.
        /// This is optional and may be null if the payment is not linked to a user.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the coupon applied to the payment, if any.
        /// </summary>
        public int? CouponId { get; set; }

        /// <summary>
        /// Gets or sets the details of the coupon applied to the payment, if any.
        /// </summary>
        public Coupon? Coupon { get; set; }

        /// <summary>
        /// Gets or sets the coupon code associated with the payment, if any.
        /// This can be used for tracking the specific coupon applied.
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the discount amount applied to the payment as a result of the coupon, if any.
        /// </summary>
        public double? Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the payment after applying any discounts.
        /// </summary>
        public double? Total { get; set; }

        /// <summary>
        /// Gets or sets the name of the person or entity making the payment.
        /// This can be used for display purposes or receipts.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the payment details were last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the current status of the payment.
        /// This can be "Pending", "Completed", "Failed", etc.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the payment intent identifier from the payment gateway, if applicable.
        /// This is typically used for tracking and managing the payment process.
        /// </summary>
        public string? PaymentIntentId { get; set; }

        /// <summary>
        /// Gets or sets the session identifier from Stripe, if the payment was processed through Stripe.
        /// </summary>
        public string? StripeSessionId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the auction associated with this payment, if applicable.
        /// This links the payment to a specific auction.
        /// </summary>
        public Guid? AuctionId { get; set; }
    }
}
