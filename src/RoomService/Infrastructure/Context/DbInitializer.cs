using Microsoft.EntityFrameworkCore;
using RoomService.Domain.Models;

namespace RoomService.Infrastructure.Context;

/// <summary>
/// Provides functionality to initialize and seed the database at application startup.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Initializes and seeds the auction database. This method is called during the application startup.
    /// It ensures that the database is created, applies any pending migrations, and seeds the database with initial data if necessary.
    /// </summary>
    /// <param name="app">The instance of <see cref="WebApplication"/> to access application services.</param>
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
    }

    /// <summary>
    /// Seeds the database with initial data if it has not been seeded already.
    /// This includes creating predefined auction items with associated details.
    /// </summary>
    /// <param name="context">The database context instance for accessing the auctions database.</param>
    private static void SeedData(AuctionDbContext context)
    {
        context.Database.Migrate();

        if (context.Auctions.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var auctions = new List<Auction>();

        context.Auctions.AddRange(auctions);

        context.SaveChanges();
    }
}