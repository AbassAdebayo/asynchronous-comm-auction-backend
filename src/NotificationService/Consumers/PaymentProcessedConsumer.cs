using SharedKernel;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

/// <summary>
/// Consumes messages indicating that a payment has been processed. This consumer receives the
/// PaymentProcessed message, logs its receipt, and notifies all connected clients via SignalR.
/// </summary>
public class PaymentProcessedConsumer : IConsumer<PaymentProcessed>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentProcessedConsumer"/> class.
    /// </summary>
    /// <param name="hubContext">The hub context for the SignalR notifications.</param>
    public PaymentProcessedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <summary>
    /// Handles the reception of a PaymentProcessed message. This method logs the message reception
    /// and notifies all clients connected through SignalR about the payment processing.
    /// </summary>
    /// <param name="context">The consume context containing the message data.</param>
    /// <returns>A task that represents the asynchronous operation of message consumption and client notification.</returns>
    public async Task Consume(ConsumeContext<PaymentProcessed> context)
    {
        Console.WriteLine($"--> Payment processed: message received");
        await _hubContext.Clients.All.SendAsync("PaymentProcessed", context.Message);
    }
}
