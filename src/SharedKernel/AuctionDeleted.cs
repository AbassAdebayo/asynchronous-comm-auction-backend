namespace SharedKernel
{
    /// <summary>
    /// Represents the details of an auction when it is deleted.
    /// This class is used to transfer auction data within the system or between systems.
    /// </summary>
    public class AuctionDeleted
    {
        /// <summary>
        /// Gets or sets the unique identifier for the auction that was deleted.
        /// </summary>
        public string Id { get; set; }
    }
}
