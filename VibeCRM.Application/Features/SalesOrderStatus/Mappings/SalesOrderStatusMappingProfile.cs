using AutoMapper;
using VibeCRM.Application.Features.SalesOrderStatus.Commands.CreateSalesOrderStatus;
using VibeCRM.Application.Features.SalesOrderStatus.Commands.UpdateSalesOrderStatus;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between SalesOrderStatus entities and DTOs.
    /// </summary>
    public class SalesOrderStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderStatusMappingProfile"/> class.
        /// Configures mappings between SalesOrderStatus entities and DTOs.
        /// </summary>
        public SalesOrderStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.SalesOrderStatus, SalesOrderStatusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderStatusId));

            CreateMap<Domain.Entities.TypeStatusEntities.SalesOrderStatus, SalesOrderStatusListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderStatusId))
                .ForMember(dest => dest.SalesOrderCount, opt => opt.Ignore()); // This will be populated separately

            CreateMap<Domain.Entities.TypeStatusEntities.SalesOrderStatus, SalesOrderStatusDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderStatusId))
                .ForMember(dest => dest.SalesOrderCount, opt => opt.Ignore()); // This will be populated separately

            // Command to Entity mappings
            CreateMap<CreateSalesOrderStatusCommand, Domain.Entities.TypeStatusEntities.SalesOrderStatus>()
                .ForMember(dest => dest.SalesOrderStatusId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateSalesOrderStatusCommand, Domain.Entities.TypeStatusEntities.SalesOrderStatus>()
                .ForMember(dest => dest.SalesOrderStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            // DTO to Entity mappings
            CreateMap<SalesOrderStatusDto, Domain.Entities.TypeStatusEntities.SalesOrderStatus>()
                .ForMember(dest => dest.SalesOrderStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}