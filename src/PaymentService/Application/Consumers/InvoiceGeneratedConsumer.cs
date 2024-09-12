using MassTransit;
using PaymentService.Application.Contracts;
using PaymentService.Domain.AggregateModels;
using PaymentService.Infrastructure;
using SharedKernel;

namespace PaymentService.Application.Consumers
{
    /// <summary>
    /// Consumes <see cref="InvoiceGenerated"/> messages to process payments based on the invoice details,
    /// confirm the payment, and publish a <see cref="PaymentProcessed"/> event to notify the Auction Service.
    /// </summary>
    public class InvoiceGeneratedConsumer : IConsumer<InvoiceGenerated>
    {
        private readonly PaymentDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IPaymentGateway _paymentGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceGeneratedConsumer"/> class with a database context, 
        /// payment gateway service, and event publisher.
        /// </summary>
        /// <param name="dbContext">The database context used for data access and manipulation.</param>
        /// <param name="publishEndpoint">The endpoint used to publish events to the message bus.</param>
        /// <param name="paymentGateway">The service used to process payments.</param>
        public InvoiceGeneratedConsumer(PaymentDbContext dbContext, IPublishEndpoint publishEndpoint, IPaymentGateway paymentGateway)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
            _paymentGateway = paymentGateway;
        }

        /// <summary>
        /// Asynchronously handles an <see cref="InvoiceGenerated"/> message, processes the payment,
        /// and publishes a <see cref="PaymentProcessed"/> event upon successful transaction.
        /// </summary>
        /// <param name="context">The consume context providing access to the <see cref="InvoiceGenerated"/> message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Consume(ConsumeContext<InvoiceGenerated> context)
        {
            Console.WriteLine("--> Consuming InvoiceGenerated event");

            var invoiceGenerated = context.Message;

            // Process the payment using a payment gateway
            var paymentResult = await _paymentGateway.MakePayment(invoiceGenerated.WinningBidAmount, invoiceGenerated.InvoiceId.ToString(), invoiceGenerated.HighestBidder.Email);

            // Determine payment status
            var paymentStatus = paymentResult.Status ? "Success" : "Failed";
            if (paymentStatus == "Failed")
            {
                Console.WriteLine($"--> Payment failed for Invoice ID: {invoiceGenerated.InvoiceId}. Reason: {paymentResult.Message}");
                // Optionally, log or notify about the failure
            }

            // Create a new payment transaction record
            var paymentTransaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                AuctionId = Guid.Parse(invoiceGenerated.AuctionId),
                InvoiceId = invoiceGenerated.InvoiceId,
                AmountPaid = invoiceGenerated.WinningBidAmount,
                PaymentDate = DateTime.UtcNow,
                Status = paymentStatus // Set to "Success" or "Failed" based on paymentResult
            };

            // Save the payment transaction to the database
            await _dbContext.PaymentTransactions.AddAsync(paymentTransaction);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"--> Payment processed for Invoice ID: {invoiceGenerated.InvoiceId}, Payment ID: {paymentTransaction.Id}, Status: {paymentTransaction.Status}");

            // Create the PaymentProcessed event
            var paymentProcessedEvent = new PaymentProcessed
            {
                PaymentId = paymentTransaction.Id,
                AuctionId = paymentTransaction.AuctionId.ToString(),
                InvoiceId = paymentTransaction.InvoiceId,
                AmountPaid = paymentTransaction.AmountPaid,
                PaymentDate = paymentTransaction.PaymentDate,
                Status = paymentTransaction.Status
            };

            // Publish the PaymentProcessed event
            await _publishEndpoint.Publish(paymentProcessedEvent);

            Console.WriteLine($"--> PaymentProcessed event published for Payment ID: {paymentTransaction.Id}, Status: {paymentTransaction.Status}");
        }
    }
}
