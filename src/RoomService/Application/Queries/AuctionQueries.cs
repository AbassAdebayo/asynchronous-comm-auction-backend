using AutoMapper;
using MediatR;
using RoomService.Application.Contracts;
using RoomService.Application.DTOs;

namespace RoomService.Application.Queries;

public class AuctionQueries
{
    public record Query : IRequest<List<AuctionResponse>>
    {
        public string Date { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<AuctionResponse>>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public Handler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<List<AuctionResponse>> Handle(Query request, CancellationToken cancellationToken)
        {

            var auctions = await _auctionRepository.GetAuctionsAsync(request.Date);

            if (auctions == null || !auctions.Any())
            {
                return new List<AuctionResponse>();
            }

            return _mapper.Map<List<AuctionResponse>>(auctions);
        }
    }
}
