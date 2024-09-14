using AutoMapper;
using InvoiceService.Domain.Models;
using InvoiceService.Infrastructure.Context;
using MassTransit;
using SharedKernel;

namespace InvoiceService.Application.Consumers
{
    /// <summary>
    /// Consumes <see cref="AuctionWinnerNotified"/> messages to generate and save an invoice in the database,
    /// and publishes an <see cref="InvoiceGenerated"/> event to notify the Payment Service.
    /// </summary>
    public class AuctionWinnerNotifiedConsumer : IConsumer<AuctionWinnerNotified>
    {
        private readonly InvoiceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionWinnerNotifiedConsumer"/> class with a database context and mapper.
        /// </summary>
        /// <param name="dbContext">The database context used for data access and manipulation.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between models.</param>
        /// <param name="publishEndpoint">The endpoint used to publish events to the message bus.</param>
        public AuctionWinnerNotifiedConsumer(InvoiceDbContext dbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Asynchronously handles an <see cref="AuctionWinnerNotified"/> message, generating and saving the corresponding invoice.
        /// and publishing an <see cref="InvoiceGenerated"/> event to the Payment Service.
        /// </summary>
        /// <param name="context">The consume context providing access to the <see cref="AuctionWinnerNotified"/> message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Consume(ConsumeContext<AuctionWinnerNotified> context)
        {
            Console.WriteLine("--> Consuming AuctionWinnerNotified event");

            var auctionWinnerNotified = context.Message;

            // Map AuctionWinnerNotified to Invoice
            var invoice = _mapper.Map<Invoice>(auctionWinnerNotified);

            // Create a new invoice
            invoice.InvoiceId = Guid.NewGuid(); // Set a new unique ID for the invoice

            // Save the invoice to the database
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();

            // Create the InvoiceGenerated event
            var invoiceGeneratedEvent = _mapper.Map<InvoiceGenerated>(invoice);

            // Publish the InvoiceGenerated event
            await _publishEndpoint.Publish(invoiceGeneratedEvent);

            Console.WriteLine($"--> InvoiceGenerated event published for Invoice ID: {invoice.InvoiceId}");
        }
    }
}
