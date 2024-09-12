using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomService.Application.Contracts;
using RoomService.Application.Models;
using RoomService.Domain.Models;
using RoomService.Infrastructure.Context;

namespace RoomService.Infrastructure.Repositories;

/// <summary>
/// Repository for managing auction data, providing a layer of abstraction over database operations.
/// This class uses Entity Framework Core for data access and AutoMapper for object mapping.
/// </summary>
public class AuctionRepository : IAuctionRepository
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuctionRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    /// <param name="mapper">The AutoMapper instance used for mapping between entities and DTOs.</param>
    public AuctionRepository(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adds a new auction to the database.
    /// </summary>
    /// <param name="auction">The <see cref="Auction"/> entity to add.</param>
    public void AddAuction(Auction auction)
    {
        _context.Auctions.Add(auction);
    }

    /// <summary>
    /// Retrieves a single auction by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the auction.</param>
    /// <returns>An <see cref="AuctionDto"/> representing the auction, or null if not found.</returns>
    public async Task<AuctionDto> GetAuctionByIdAsync(Guid id)
    {
        var auction =  await _context.Auctions
            .FirstOrDefaultAsync(x => x.Id == id);

        var auctionDtos = _mapper.Map<AuctionDto>(auction);
        return auctionDtos;
    }

    /// <summary>
    /// Retrieves the auction entity by its unique identifier, including related entities as necessary.
    /// </summary>
    /// <param name="id">The unique identifier of the auction.</param>
    /// <returns>An <see cref="Auction"/> entity, or null if not found.</returns>
    public async Task<Auction> GetAuctionEntityById(Guid id)
    {
        return await _context.Auctions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Retrieves a list of auctions, optionally filtering by update date.
    /// </summary>
    /// <param name="date">The date to filter auctions by their updated timestamp. Nullable.</param>
    /// <returns>A list of <see cref="AuctionDto"/> representing the auctions.</returns>
    public async Task<List<AuctionDto>> GetAuctionsAsync(string date)
    {
        var query = _context.Auctions.OrderBy(x => x.CreatedAt).AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        var auctions = await query.ToListAsync();
        var auctionDtos = _mapper.Map<List<AuctionDto>>(auctions);
        return auctionDtos;
    }

    /// <summary>
    /// Removes an auction from the database.
    /// </summary>
    /// <param name="auction">The <see cref="Auction"/> entity to remove.</param>
    public void RemoveAuction(Auction auction)
    {
        _context.Auctions.Remove(auction);
    }

    /// <summary>
    /// Saves all changes made in the context to the database.
    /// </summary>
    /// <returns>True if the save operation was successful; otherwise, false.</returns>
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
