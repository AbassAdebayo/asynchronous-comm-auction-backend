namespace SharedKernel
{
    /// <summary>
    /// Represents the details of an auction when it is updated.
    /// This class is used to transfer auction data within the system or between systems.
    /// </summary>
    public class AuctionUpdated
    {
        /// <summary>
        /// Gets or sets the unique identifier for the auction that was updated.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the make of the item being auctioned, if applicable.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Gets or sets the model of the item being auctioned, if applicable.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the year of manufacture for the item being auctioned, if applicable.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the color of the item being auctioned, if applicable.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the mileage of the item being auctioned, if applicable.
        /// </summary>
        public int Mileage { get; set; }
    }
}
