using AutoMapper;
using SharedKernel;
using InvoiceService.Domain.Models;

namespace InvoiceService.Application.Mappings
{
    /// <summary>
    /// Configures AutoMapper profiles for mapping between entity models and DTOs within the InvoiceService.
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfiles"/> class.
        /// </summary>
        public MappingProfiles()
        {
            // Mapping from AuctionWinnerNotified to Invoice
            CreateMap<AuctionWinnerNotified, Invoice>()
                .ForMember(dest => dest.InvoiceId, opt => opt.Ignore()) // Ignore InvoiceId, as it's auto-generated
                .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillingAddress))
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
                .ForMember(dest => dest.PaymentInstructions, opt => opt.MapFrom(src => src.PaymentInstructions))
                .ForMember(dest => dest.RefundPolicy, opt => opt.MapFrom(src => src.RefundPolicy))
                .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
                .ForMember(dest => dest.ItemDetails, opt => opt.MapFrom(src => src.ItemDetails))
                .ForMember(dest => dest.HighestBidder, opt => opt.MapFrom(src => src.HighestBidder))
                .ForMember(dest => dest.WinningBidAmount, opt => opt.MapFrom(src => src.WinningBidAmount))
                .ForMember(dest => dest.PaymentTerms, opt => opt.MapFrom(src => src.PaymentTerms))
                .ForMember(dest => dest.AuctionCompletionDate, opt => opt.MapFrom(src => src.AuctionCompletionDate));

            // Mapping from AuctionWinnerNotified to Invoice
            CreateMap<Invoice, InvoiceGenerated>()
                .ForMember(dest => dest.InvoiceId, opt => opt.Ignore()) // Ignore InvoiceId, as it's auto-generated
                .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillingAddress))
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
                .ForMember(dest => dest.PaymentInstructions, opt => opt.MapFrom(src => src.PaymentInstructions))
                .ForMember(dest => dest.RefundPolicy, opt => opt.MapFrom(src => src.RefundPolicy))
                .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
                .ForMember(dest => dest.ItemDetails, opt => opt.MapFrom(src => src.ItemDetails))
                .ForMember(dest => dest.HighestBidder, opt => opt.MapFrom(src => src.HighestBidder))
                .ForMember(dest => dest.WinningBidAmount, opt => opt.MapFrom(src => src.WinningBidAmount))
                .ForMember(dest => dest.PaymentTerms, opt => opt.MapFrom(src => src.PaymentTerms));
        }
    }
}
