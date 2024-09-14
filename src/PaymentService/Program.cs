using Microsoft.Data.SqlClient;
using PaymentService.Infrastructure.IOC.Extensions;
using Polly;
using Serilog;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
// Add services to the container.
builder.Services
       .AddDatabase(builder.Configuration)
       .AddHttpClient()
       .AddRepositories()
       .AddCustomIntegrationTransport(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

var app = builder.Build();

app.UseSerilogRequestLogging();

var retryPolicy = Policy.Handle<SqlException>().WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(10));

app.Run();