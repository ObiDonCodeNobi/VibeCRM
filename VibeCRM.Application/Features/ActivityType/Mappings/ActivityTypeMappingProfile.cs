using AutoMapper;
using VibeCRM.Application.Features.ActivityType.Commands.CreateActivityType;
using VibeCRM.Application.Features.ActivityType.Commands.UpdateActivityType;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ActivityType entities and DTOs.
    /// </summary>
    public class ActivityTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypeMappingProfile"/> class.
        /// Configures mappings between ActivityType entities and DTOs.
        /// </summary>
        public ActivityTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.ActivityType, ActivityTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.ActivityType, ActivityTypeListDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.ActivityType, ActivityTypeDetailsDto>();

            // Command to Entity mappings
            CreateMap<CreateActivityTypeCommand, Domain.Entities.TypeStatusEntities.ActivityType>();
            CreateMap<UpdateActivityTypeCommand, Domain.Entities.TypeStatusEntities.ActivityType>();

            // DTO to Entity mappings
            CreateMap<ActivityTypeDto, Domain.Entities.TypeStatusEntities.ActivityType>();
        }
    }
}