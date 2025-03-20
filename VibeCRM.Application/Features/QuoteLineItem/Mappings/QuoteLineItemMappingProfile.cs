using AutoMapper;
using VibeCRM.Application.Features.QuoteLineItem.Commands.CreateQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.Commands.UpdateQuoteLineItem;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;

namespace VibeCRM.Application.Features.QuoteLineItem.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between QuoteLineItem entities and DTOs
    /// </summary>
    public class QuoteLineItemMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteLineItemMappingProfile"/> class
        /// </summary>
        public QuoteLineItemMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.QuoteLineItem, QuoteLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteLineItemId));

            CreateMap<Domain.Entities.BusinessEntities.QuoteLineItem, QuoteLineItemDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteLineItemId))
                .ForMember(dest => dest.ExtendedPrice, opt => opt.MapFrom(src => src.GetExtendedPrice()))
                .ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.GetDiscountAmount()))
                .ForMember(dest => dest.TaxAmount, opt => opt.MapFrom(src => src.GetTaxAmount()))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()));

            CreateMap<Domain.Entities.BusinessEntities.QuoteLineItem, QuoteLineItemListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteLineItemId))
                .ForMember(dest => dest.ExtendedPrice, opt => opt.MapFrom(src => src.GetExtendedPrice()));

            // Command to Entity mappings
            CreateMap<CreateQuoteLineItemCommand, Domain.Entities.BusinessEntities.QuoteLineItem>()
                .ForMember(dest => dest.QuoteLineItemId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UpdateQuoteLineItemCommand, Domain.Entities.BusinessEntities.QuoteLineItem>()
                .ForMember(dest => dest.QuoteLineItemId, opt => opt.MapFrom(src => src.Id));
        }
    }
}