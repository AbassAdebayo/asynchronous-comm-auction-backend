using AutoMapper;
using BiddingService.Application.Commands;
using BiddingService.Application.Models;
using BiddingService.Application.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace BiddingService.Controllers
{
    /// <summary>
    /// Handles bid-related actions, allowing users to place bids on auctions and retrieve bids for specific auctions.
    /// Utilizes gRPC for auction validation, AutoMapper for DTO mapping, and MassTransit for event publishing.
    /// </summary>
    [ApiController, Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidsController"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for mapping between models and DTOs.</param>
        /// <param name="publishEndpoint">The MassTransit publish endpoint for publishing events.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        public BidsController(IMapper mapper, IPublishEndpoint publishEndpoint, IMediator mediator)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        /// <summary>
        /// Places a bid on an auction, verifying the auction's existence and eligibility for bidding through gRPC.
        /// Publishes a bid placed event if the bid is successful.
        /// </summary>
        /// <param name="auctionId">The ID of the auction to bid on.</param>
        /// <param name="amount">The bid amount.</param>
        /// <returns>The placed bid's details or an error message if the bid cannot be placed.</returns>
        [Authorize, HttpPost("{auctionId}/bids")]
        public async Task<ActionResult<BidDto>> PlaceBid(string auctionId, [FromBody] int amount)
        {
            var command = new PlaceBidCommand
            {
                AuctionId = auctionId,
                Amount = amount,
                Bidder = User.Identity.Name
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest("Cannot place bid on this auction.");
            }

            await _publishEndpoint.Publish(_mapper.Map<BidPlaced>(result));

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all bids for a given auction, sorted by bid date in descending order.
        /// </summary>
        /// <param name="auctionId">The ID of the auction for which to retrieve bids.</param>
        /// <returns>A list of bids for the auction.</returns>
        [HttpGet("{auctionId}/bids")]
        public async Task<ActionResult<List<BidResponse>>> GetBidsForAuction(string auctionId)
        {
            var result = await _mediator.Send(new GetBidsForAuctionQuery { AuctionId = auctionId });

            if (result == null || !result.Any())
            {
                return NotFound("No bids found for this auction.");
            }

            return Ok(result);
        }
    }
}
