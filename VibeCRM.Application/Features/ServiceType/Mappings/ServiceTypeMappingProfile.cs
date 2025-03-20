using AutoMapper;
using VibeCRM.Application.Features.ServiceType.Commands.CreateServiceType;
using VibeCRM.Application.Features.ServiceType.Commands.UpdateServiceType;
using VibeCRM.Application.Features.ServiceType.DTOs;

namespace VibeCRM.Application.Features.ServiceType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ServiceType entities and DTOs.
    /// </summary>
    public class ServiceTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeMappingProfile"/> class.
        /// Configures mappings between ServiceType entities and DTOs.
        /// </summary>
        public ServiceTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.ServiceType, ServiceTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.ServiceType, ServiceTypeListDto>()
                .ForMember(dest => dest.ServiceCount, opt => opt.MapFrom(src => src.Services != null ? src.Services.Count : 0));
            CreateMap<Domain.Entities.TypeStatusEntities.ServiceType, ServiceTypeDetailsDto>()
                .ForMember(dest => dest.ServiceCount, opt => opt.MapFrom(src => src.Services != null ? src.Services.Count : 0));

            // Command to Entity mappings
            CreateMap<CreateServiceTypeCommand, Domain.Entities.TypeStatusEntities.ServiceType>()
                .ForMember(dest => dest.Services, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<UpdateServiceTypeCommand, Domain.Entities.TypeStatusEntities.ServiceType>()
                .ForMember(dest => dest.Services, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}
