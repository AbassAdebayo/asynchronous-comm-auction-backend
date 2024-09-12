using AutoMapper;
using BiddingService.Application.Models;
using BiddingService.Domain.AggregateModels.AuctionAggregate;
using BiddingService.Domain.AggregateModels.BiddingAggregate;
using BiddingService.Exceptions;
using MassTransit;
using MediatR;
using MongoDB.Entities;
using SharedKernel;

namespace BiddingService.Application.Commands
{
    public class PlaceBidCommand : IRequest<BidResponse>
    {
        public string AuctionId { get; init; }
        public int Amount { get; init; }
        public string Bidder { get; init; }
    }

    public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, BidResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public PlaceBidCommandHandler(IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<BidResponse> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var auction = await DB.Find<Auction>()
                                  .OneAsync(request.AuctionId, cancellationToken);

            if (auction == null)
                throw new BadRequestException("Auction not found");

            ValidateAuctionState(auction, request);

            var bid = CreateBid(request, auction);
            SetBidStatus(bid, auction, cancellationToken);

            await SaveBidAsync(bid);
            await PublishBidPlacedEventAsync(bid, cancellationToken);

            return _mapper.Map<BidResponse>(bid);
        }

        #region Private Methods
        private void ValidateRequest(PlaceBidCommand request)
        {
            if (string.IsNullOrEmpty(request.AuctionId))
                throw new ArgumentException("AuctionId cannot be null or empty", nameof(request.AuctionId));

            if (string.IsNullOrEmpty(request.Bidder))
                throw new ArgumentException("Bidder cannot be null or empty", nameof(request.Bidder));

            if (request.Amount <= 0)
                throw new ArgumentException("Bid amount must be greater than zero", nameof(request.Amount));
        }

        private void ValidateAuctionState(Auction auction, PlaceBidCommand request)
        {
            if (auction.Seller == request.Bidder)
                throw new BadRequestException("You cannot bid on your own auction");

            if (auction.AuctionEnd < DateTime.UtcNow)
                throw new BadRequestException("Cannot accept bids on this auction at this time");
        }

        private Bid CreateBid(PlaceBidCommand request, Auction auction)
        {
            return new Bid
            {
                Amount = request.Amount,
                AuctionId = request.AuctionId,
                Bidder = request.Bidder,
                BidStatus = auction.AuctionEnd < DateTime.UtcNow ? BidStatus.Finished : BidStatus.TooLow,
                BidTime = DateTime.UtcNow
            };
        }

        private async void SetBidStatus(Bid bid, Auction auction, CancellationToken cancellationToken)
        {
            var highBid = await DB.Find<Bid>()
                 .Match(a => a.AuctionId == bid.AuctionId)
                 .Sort(b => b.Descending(x => x.Amount))
                 .ExecuteFirstAsync();

            if (highBid == null || bid.Amount > highBid.Amount)
            {
                bid.BidStatus = bid.Amount > auction.ReservePrice
                    ? BidStatus.Accepted
                    : BidStatus.AcceptedBelowReserve;
            }
            else
            {
                bid.BidStatus = BidStatus.TooLow;
            }
        }

        private async Task SaveBidAsync(Bid bid)
        {
            await DB.SaveAsync(bid);
        }

        private async Task PublishBidPlacedEventAsync(Bid bid, CancellationToken cancellationToken)
        {
            var bidPlacedEvent = _mapper.Map<BidPlaced>(bid);
            await _publishEndpoint.Publish(bidPlacedEvent, cancellationToken);
        } 
        #endregion
    }
}
