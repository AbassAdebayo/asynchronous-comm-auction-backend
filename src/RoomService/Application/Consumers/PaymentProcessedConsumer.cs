using MassTransit;
using RoomService.Domain.Models;
using RoomService.Infrastructure.Context;
using SharedKernel;

namespace RoomService.Application.Consumers
{
    /// <summary>
    /// Consumes <see cref="PaymentProcessed"/> messages to update the auction status
    /// based on the payment result.
    /// </summary>
    public class PaymentProcessedConsumer : IConsumer<PaymentProcessed>
    {
        private readonly AuctionDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProcessedConsumer"/> class with a database context.
        /// </summary>
        /// <param name="dbContext">The database context used for data access and manipulation.</param>
        public PaymentProcessedConsumer(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Asynchronously handles a <see cref="PaymentProcessed"/> message, updating the corresponding auction's status
        /// based on the payment outcome.
        /// </summary>
        /// <param name="context">The consume context providing access to the <see cref="PaymentProcessed"/> message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Consume(ConsumeContext<PaymentProcessed> context)
        {
            Console.WriteLine("--> Consuming PaymentProcessed event");

            var paymentProcessed = context.Message;

            var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(paymentProcessed.AuctionId));

            if (auction == null)
            {
                Console.WriteLine($"--> Auction with ID {paymentProcessed.AuctionId} not found.");
                return;
            }

            // Update auction status based on payment status
            auction.Status = paymentProcessed.Status switch
            {
                "Success" => AuctionStatus.Paid,
                "Failed" => AuctionStatus.Failed,
                "Disputed" => AuctionStatus.Disputed,
                _ => AuctionStatus.PaymentPending,
            };

            // Save changes to the database
            _dbContext.Auctions.Update(auction);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"--> Auction with ID {auction.Id} updated to status {auction.Status}");
        }
    }
}
