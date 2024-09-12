using AutoMapper;
using MediatR;
using RoomService.Application.Contracts;
using RoomService.Exceptions;

namespace RoomService.Application.Commands;

public class UpdateAuction
{
    public class UpdateAuctionCommand : IRequest<UpdateAuctionResponse>
    {
        public Guid Id { get; set; }
        public string Make { get; init; }
        public string Model { get; init; }
        public int? Year { get; init; }
        public string Color { get; init; }
        public int? Mileage { get; init; }
    }

    public record UpdateAuctionResponse(

        string Make,
        string Model,
        int Year,
        string Color,
        int Mileage,
        string ItemImageUrl);

    public class Handler : IRequestHandler<UpdateAuctionCommand, UpdateAuctionResponse>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public Handler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<UpdateAuctionResponse> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _auctionRepository.GetAuctionEntityById(request.Id);
            if (auction == null)
            {
                throw new NotFoundException("Auction not found.");
            }


            auction.UpdateDetails(
                request.Make,
                request.Model,
                request.Year,
                request.Color,
                request.Mileage);

            await _auctionRepository.SaveChangesAsync();

            return _mapper.Map<UpdateAuctionResponse>(auction);
        }
    }
}
