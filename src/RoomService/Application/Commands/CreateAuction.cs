using AutoMapper;
using MediatR;
using RoomService.Application.Contracts;
using RoomService.Application.Models;
using RoomService.Domain.Models;

namespace RoomService.Application.Commands;
public class CreateAuction
{
    public class CreateAuctionCommand : IRequest<AuctionResponse>
    {
        public Guid Id { get; init; }
        public int ReservePrice { get; init; }
        public string Seller { get; init; }
        public string Winner { get; init; }
        public decimal? SoldAmount { get; init; }
        public int? CurrentHighBid { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
        public DateTime AuctionEnd { get; init; }
        public AuctionStatus Status { get; init; }
        public Item Item { get; init; }
    }

    public class Handler : IRequestHandler<CreateAuctionCommand, AuctionResponse>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public Handler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<AuctionResponse> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {

            var auction = new Auction
            {
                Id = request.Id,
                ReservePrice = request.ReservePrice,
                Seller = request.Seller,
                Winner = request.Winner,
                SoldAmount = request.SoldAmount,
                CurrentHighBid = request.CurrentHighBid,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                AuctionEnd = request.AuctionEnd,
                Status = request.Status,
                Item = request.Item
            };
            _auctionRepository.AddAuction(auction);
            await _auctionRepository.SaveChangesAsync();

            return _mapper.Map<AuctionResponse>(auction);
        }
    }
}


