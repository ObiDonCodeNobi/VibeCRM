using AutoMapper;
using VibeCRM.Application.Features.ProductType.Commands.CreateProductType;
using VibeCRM.Application.Features.ProductType.Commands.UpdateProductType;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Features.ProductType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ProductType entities and DTOs.
    /// </summary>
    public class ProductTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeMappingProfile"/> class.
        /// Configures mappings between ProductType entities and DTOs.
        /// </summary>
        public ProductTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.ProductType, ProductTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.ProductType, ProductTypeListDto>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0));
            CreateMap<Domain.Entities.TypeStatusEntities.ProductType, ProductTypeDetailsDto>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0));

            // Command to Entity mappings
            CreateMap<CreateProductTypeCommand, Domain.Entities.TypeStatusEntities.ProductType>()
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<UpdateProductTypeCommand, Domain.Entities.TypeStatusEntities.ProductType>()
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}