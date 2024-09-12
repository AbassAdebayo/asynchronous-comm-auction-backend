using MediatR;
using RoomService.Application.Contracts;

namespace RoomService.Application.Commands;

public class DeleteAuction
{

    public class DeleteAuctionCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string CurrentUser { get; init; }
    }


    public class Handler : IRequestHandler<DeleteAuctionCommand, bool>
    {
        private readonly IAuctionRepository _auctionRepository;

        public Handler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<bool> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
        {

            var auction = await _auctionRepository.GetAuctionEntityById(request.Id);
            if (auction == null)
            {
                return false;
            }

            var currentUser = request.CurrentUser;
            if (auction.Seller != currentUser)
            {
                return false;
            }
            _auctionRepository.RemoveAuction(auction);
            return await _auctionRepository.SaveChangesAsync();
        }
    }
}
