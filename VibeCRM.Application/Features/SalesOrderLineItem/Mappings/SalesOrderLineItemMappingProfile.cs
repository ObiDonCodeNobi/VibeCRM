using AutoMapper;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.CreateSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.UpdateSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between SalesOrderLineItem entities and DTOs
    /// </summary>
    public class SalesOrderLineItemMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderLineItemMappingProfile"/> class
        /// </summary>
        public SalesOrderLineItemMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.SalesOrderLineItem, SalesOrderLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderLineItemId));

            CreateMap<Domain.Entities.BusinessEntities.SalesOrderLineItem, SalesOrderLineItemDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderLineItemId))
                .ForMember(dest => dest.ExtendedPrice, opt => opt.MapFrom(src => src.GetExtendedPrice()))
                .ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.GetDiscountAmount()))
                .ForMember(dest => dest.TaxAmount, opt => opt.MapFrom(src => src.GetTaxAmount()))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()));

            CreateMap<Domain.Entities.BusinessEntities.SalesOrderLineItem, SalesOrderLineItemListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderLineItemId))
                .ForMember(dest => dest.ExtendedPrice, opt => opt.MapFrom(src => src.GetExtendedPrice()));

            // Command to Entity mappings
            CreateMap<CreateSalesOrderLineItemCommand, Domain.Entities.BusinessEntities.SalesOrderLineItem>()
                .ForMember(dest => dest.SalesOrderLineItemId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UpdateSalesOrderLineItemCommand, Domain.Entities.BusinessEntities.SalesOrderLineItem>()
                .ForMember(dest => dest.SalesOrderLineItemId, opt => opt.MapFrom(src => src.Id));
        }
    }
}