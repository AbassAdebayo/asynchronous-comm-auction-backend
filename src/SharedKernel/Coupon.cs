namespace SharedKernel
{
    /// <summary>
    /// Represents a coupon entity.
    /// Used for storing information about discount coupons applicable to payments or purchases.
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// Gets or sets the unique identifier for the coupon.
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// Gets or sets the code associated with the coupon.
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the discount amount provided by the coupon.
        /// </summary>
        public double? DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the minimum amount required to apply the coupon.
        /// </summary>
        public int? MinAmount { get; set; }
    }
}
