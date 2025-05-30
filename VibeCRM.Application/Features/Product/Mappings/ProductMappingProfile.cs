using AutoMapper;
using VibeCRM.Shared.DTOs.Product;
using VibeCRM.Shared.DTOs.ProductGroup;
using VibeCRM.Shared.DTOs.ProductType;
using VibeCRM.Shared.DTOs.QuoteLineItem;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.Product.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Product entities and DTOs
    /// </summary>
    public class ProductMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMappingProfile"/> class.
        /// </summary>
        public ProductMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<Domain.Entities.BusinessEntities.Product, ProductDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                .ForMember(dest => dest.QuoteLineItems, opt => opt.MapFrom(src => src.QuoteLineItems))
                .ForMember(dest => dest.SalesOrderLineItems, opt => opt.MapFrom(src => src.SalesOrderLineItems))
                .ForMember(dest => dest.ProductGroups, opt => opt.MapFrom(src => src.ProductGroups));

            CreateMap<Domain.Entities.BusinessEntities.Product, ProductListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType != null ? src.ProductType.Type : null))
                .ForMember(dest => dest.QuoteLineItemCount, opt => opt.MapFrom(src => src.QuoteLineItems != null ? src.QuoteLineItems.Count : 0))
                .ForMember(dest => dest.SalesOrderLineItemCount, opt => opt.MapFrom(src => src.SalesOrderLineItems != null ? src.SalesOrderLineItems.Count : 0))
                .ForMember(dest => dest.ProductGroupCount, opt => opt.MapFrom(src => src.ProductGroups != null ? src.ProductGroups.Count : 0));

            // Related entity mappings
            CreateMap<Domain.Entities.TypeStatusEntities.ProductType, ProductTypeDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductTypeId));

            CreateMap<Domain.Entities.BusinessEntities.ProductGroup, ProductGroupListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductGroupId));

            CreateMap<Domain.Entities.BusinessEntities.QuoteLineItem, QuoteLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuoteLineItemId));

            CreateMap<Domain.Entities.BusinessEntities.SalesOrderLineItem, SalesOrderLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderLineItemId));
        }
    }
}