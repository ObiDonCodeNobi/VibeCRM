using AutoMapper;
using VibeCRM.Application.Features.ProductGroup.Commands.CreateProductGroup;
using VibeCRM.Application.Features.ProductGroup.Commands.UpdateProductGroup;
using VibeCRM.Application.Features.ProductGroup.DTOs;

namespace VibeCRM.Application.Features.ProductGroup.Mappings
{
    /// <summary>
    /// Mapping profile for ProductGroup entity and related DTOs.
    /// Defines how entities are converted to DTOs and vice versa.
    /// </summary>
    public class ProductGroupMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductGroupMappingProfile"/> class
        /// and configures the entity-to-DTO mappings.
        /// </summary>
        public ProductGroupMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.ProductGroup, ProductGroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductGroupId));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.ProductGroup, ProductGroupDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductGroupId))
                .ForMember(dest => dest.ParentProductGroupName, opt => opt.Ignore())
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0))
                .ForMember(dest => dest.ChildGroupCount, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByName, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedByName, opt => opt.Ignore());

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.ProductGroup, ProductGroupListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductGroupId))
                .ForMember(dest => dest.ParentProductGroupName, opt => opt.Ignore())
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0))
                .ForMember(dest => dest.ChildGroupCount, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreateProductGroupCommand, VibeCRM.Domain.Entities.BusinessEntities.ProductGroup>()
                .ForMember(dest => dest.ProductGroupId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Products, opt => opt.Ignore());

            CreateMap<UpdateProductGroupCommand, VibeCRM.Domain.Entities.BusinessEntities.ProductGroup>()
                .ForMember(dest => dest.ProductGroupId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}