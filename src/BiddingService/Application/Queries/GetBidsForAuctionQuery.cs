using AutoMapper;
using BiddingService.Application.Models;
using BiddingService.Domain.AggregateModels.BiddingAggregate;
using MediatR;
using MongoDB.Entities;

namespace BiddingService.Application.Queries
{
    public class GetBidsForAuctionQuery : IRequest<List<BidResponse>>
    {
        public string AuctionId { get; init; }
    }

    public class GetBidsForAuctionQueryHandler : IRequestHandler<GetBidsForAuctionQuery, List<BidResponse>>
    {
        private readonly IMapper _mapper;

        public GetBidsForAuctionQueryHandler(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<BidResponse>> Handle(GetBidsForAuctionQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.AuctionId))
            {
                throw new ArgumentException("AuctionId cannot be null or empty", nameof(request.AuctionId));
            }

            var bids = await DB.Find<Bid>()
                .Match(a => a.AuctionId == request.AuctionId)
                .Sort(b => b.Descending(a => a.BidTime))
                .ExecuteAsync(cancellationToken);

            if (bids == null || bids.Count == 0)
            {
                return new List<BidResponse>(); // Return an empty list if no bids are found
            }

            return _mapper.Map<List<BidResponse>>(bids);
        }
    }
}
