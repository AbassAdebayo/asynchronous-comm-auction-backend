using SharedKernel;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

/// <summary>
/// Consumes messages indicating that an invoice has been generated. This consumer receives the
/// InvoiceGenerated message, logs its receipt, and notifies all connected clients via SignalR.
/// </summary>
public class InvoiceGeneratedConsumer : IConsumer<InvoiceGenerated>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceGeneratedConsumer"/> class.
    /// </summary>
    /// <param name="hubContext">The hub context for the SignalR notifications.</param>
    public InvoiceGeneratedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <summary>
    /// Handles the reception of an InvoiceGenerated message. This method logs the message reception
    /// and notifies all clients connected through SignalR about the invoice generation.
    /// </summary>
    /// <param name="context">The consume context containing the message data.</param>
    /// <returns>A task that represents the asynchronous operation of message consumption and client notification.</returns>
    public async Task Consume(ConsumeContext<InvoiceGenerated> context)
    {
        Console.WriteLine($"--> Invoice generated: message received");
        await _hubContext.Clients.All.SendAsync("InvoiceGenerated", context.Message);
    }
}
