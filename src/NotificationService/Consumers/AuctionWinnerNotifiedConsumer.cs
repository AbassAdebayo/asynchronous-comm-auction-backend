using SharedKernel;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers
{
    /// <summary>
    /// Consumes messages indicating that an auction winner has been notified. This consumer receives the
    /// AuctionWinnerNotified message, logs its receipt, and notifies all connected clients via SignalR.
    /// </summary>
    public class AuctionWinnerNotifiedConsumer : IConsumer<AuctionWinnerNotified>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionWinnerNotifiedConsumer"/> class.
        /// </summary>
        /// <param name="hubContext">The hub context for the SignalR notifications.</param>
        public AuctionWinnerNotifiedConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Handles the reception of an AuctionWinnerNotified message. This method logs the message reception
        /// and notifies all clients connected through SignalR about the auction winner's notification.
        /// </summary>
        /// <param name="context">The consume context containing the message data.</param>
        /// <returns>A task that represents the asynchronous operation of message consumption and client notification.</returns>
        public async Task Consume(ConsumeContext<AuctionWinnerNotified> context)
        {
            Console.WriteLine($"--> Auction winner notified: message received");
            await _hubContext.Clients.All.SendAsync("AuctionWinnerNotified", context.Message);
        }
    }
}
