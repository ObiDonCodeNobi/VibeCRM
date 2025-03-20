using AutoMapper;
using VibeCRM.Application.Features.Quote.Commands.CreateQuote;
using VibeCRM.Application.Features.Quote.Commands.UpdateQuote;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.Quote.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Quote entities and DTOs
    /// </summary>
    public class QuoteMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteMappingProfile"/> class.
        /// </summary>
        public QuoteMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Quote, QuoteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteId));

            CreateMap<Domain.Entities.BusinessEntities.Quote, QuoteDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteId))
                .ForMember(dest => dest.QuoteStatusId, opt => opt.MapFrom(src => src.QuoteStatusId))
                .ForMember(dest => dest.QuoteStatusName, opt => opt.MapFrom(src => src.QuoteStatus != null ? src.QuoteStatus.Status : null))
                .ForMember(dest => dest.LineItems, opt => opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.SalesOrders, opt => opt.MapFrom(src => src.SalesOrders));

            CreateMap<Domain.Entities.BusinessEntities.Quote, QuoteListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteId))
                .ForMember(dest => dest.QuoteStatusId, opt => opt.MapFrom(src => src.QuoteStatusId))
                .ForMember(dest => dest.QuoteStatusName, opt => opt.MapFrom(src => src.QuoteStatus != null ? src.QuoteStatus.Status : null))
                .ForMember(dest => dest.LineItemCount, opt => opt.MapFrom(src => src.LineItems != null ? src.LineItems.Count : 0))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.LineItems != null ?
                    src.LineItems.Sum(li => li.UnitPrice * li.Quantity -
                        (li.DiscountAmount ?? 0) -
                        (li.UnitPrice * li.Quantity * (li.DiscountPercentage ?? 0) / 100) +
                        (li.UnitPrice * li.Quantity * (li.TaxPercentage ?? 0) / 100)) : 0));

            // QuoteLineItem to QuoteLineItemDto mapping
            CreateMap<Domain.Entities.BusinessEntities.QuoteLineItem, QuoteLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteLineItemId))
                .ForMember(dest => dest.QuoteId, opt => opt.MapFrom(src => src.QuoteId));

            // SalesOrder to SalesOrderListDto mapping
            CreateMap<Domain.Entities.BusinessEntities.SalesOrder, SalesOrderListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderId));

            // Command to Entity mappings
            CreateMap<CreateQuoteCommand, Domain.Entities.BusinessEntities.Quote>()
                .ForMember(dest => dest.QuoteId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteStatus, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrders, opt => opt.Ignore());

            CreateMap<UpdateQuoteCommand, Domain.Entities.BusinessEntities.Quote>()
                .ForMember(dest => dest.QuoteId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteStatus, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrders, opt => opt.Ignore());
        }
    }
}