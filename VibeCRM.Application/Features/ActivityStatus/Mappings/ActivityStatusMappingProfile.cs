using AutoMapper;
using VibeCRM.Application.Features.ActivityStatus.Commands.CreateActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.Commands.UpdateActivityStatus;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ActivityStatus entity and its DTOs.
    /// </summary>
    public class ActivityStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityStatusMappingProfile"/> class.
        /// Configures the mapping between ActivityStatus entities and their corresponding DTOs.
        /// </summary>
        public ActivityStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus, ActivityStatusDto>();
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus, ActivityStatusListDto>();
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus, ActivityStatusDetailsDto>();

            // DTO to Entity mappings
            CreateMap<ActivityStatusDto, VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus>();

            // Command to Entity mappings
            CreateMap<CreateActivityStatusCommand, VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => System.Guid.NewGuid()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateActivityStatusCommand, VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus>();
        }
    }
}