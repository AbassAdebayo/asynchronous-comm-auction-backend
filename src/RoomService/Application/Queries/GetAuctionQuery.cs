using AutoMapper;
using MediatR;
using RoomService.Application.Contracts;
using RoomService.Application.Models;

namespace RoomService.Application.Queries;

public partial class GetAuctionQuery
{

    public record Query(Guid Id) : IRequest<AuctionResponse>;

    public class Handler : IRequestHandler<Query, AuctionResponse>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public Handler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<AuctionResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(request.Id);

            if (auction == null)
            {
                throw new KeyNotFoundException($"Auction with ID {request.Id} was not found.");
            }

            return _mapper.Map<AuctionResponse>(auction);
        }
    }
}
