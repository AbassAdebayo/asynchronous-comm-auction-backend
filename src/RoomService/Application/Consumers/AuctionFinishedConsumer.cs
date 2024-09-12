using MassTransit;
using RoomService.Domain.Models;
using RoomService.Infrastructure.Context;
using SharedKernel;

namespace RoomService.Application.Consumers
{
    /// <summary>
    /// Consumes <see cref="AuctionFinished"/> messages to update auction entities in the database
    /// based on the auction completion details provided in the message.
    /// </summary>
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionFinishedConsumer"/> class with a database context and publish endpoint.
        /// </summary>
        /// <param name="dbContext">The database context used for data access and manipulation.</param>
        /// <param name="publishEndpoint">The publish endpoint to notify other services.</param>
        public AuctionFinishedConsumer(AuctionDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Asynchronously handles an <see cref="AuctionFinished"/> message, updating the corresponding auction's status,
        /// winner, and sold amount in the database. If the item was sold, it publishes an <see cref="AuctionWinnerNotified"/> event.
        /// </summary>
        /// <param name="context">The consume context providing access to the <see cref="AuctionFinished"/> message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// Upon receiving an <see cref="AuctionFinished"/> message, this method updates the corresponding auction record in the database.
        /// If the item was sold, it records the winner's identity and the final sold amount. Then, it updates the auction's status
        /// based on whether the sold amount meets or exceeds the reserve price. If the auction is completed successfully, 
        /// it publishes the <see cref="AuctionWinnerNotified"/> event to notify the Invoice Service.
        /// </remarks>
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            Console.WriteLine("--> Consuming auction finished");

            var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));

            if (auction == null)
            {
                Console.WriteLine($"--> Auction with ID {context.Message.AuctionId} not found.");
                return;
            }

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = context.Message.Amount;

                // Publish the AuctionWinnerNotified event to the Invoice Service
                var auctionWinnerNotified = new AuctionWinnerNotified(
                    auctionId: context.Message.AuctionId,
                    itemDetails: new AuctionItemDetails(
                        itemId: auction.Item.Id.ToString(),// auction.ItemId,
                        name: auction.Item.Model,// auction.ItemName,
                        description: auction.Item.Make // auction.ItemDescription
                    ),
                    highestBidder: new BidderInfo(
                        bidderId: context.Message.HighestBidder.BidderId,
                        fullName: context.Message.HighestBidder.FullName,
                        email: context.Message.HighestBidder.Email
                    ),
                    winningBidAmount: context.Message.WinningBidAmount,
                    paymentTerms: new PaymentTerms(
                        dueDate: DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"),
                        currency: "USD"
                    ),
                    auctionCompletionDate: DateTime.UtcNow,
                    billingAddress: context.Message.BillingAddress,
                    invoiceDate: DateTime.UtcNow,
                    paymentInstructions: "Please pay the amount due within 7 days.",
                    refundPolicy: "Refunds are available within 30 days of payment."
                );

                await _publishEndpoint.Publish(auctionWinnerNotified);
            }

            auction.Status = auction.SoldAmount > auction.ReservePrice
                ? AuctionStatus.Completed
                : AuctionStatus.ReserveNotMet;

            await _dbContext.SaveChangesAsync();
        }
    }
}