using AutoMapper;
using VibeCRM.Application.Features.Activity.Commands.CreateActivity;
using VibeCRM.Application.Features.Activity.Commands.UpdateActivity;
using VibeCRM.Application.Features.Activity.DTOs;

namespace VibeCRM.Application.Features.Activity.Mappings
{
    /// <summary>
    /// Mapping profile for Activity entity and related DTOs.
    /// Defines how entities are converted to DTOs and vice versa.
    /// </summary>
    public class ActivityMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityMappingProfile"/> class
        /// and configures the entity-to-DTO mappings.
        /// </summary>
        public ActivityMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Activity, ActivityDto>()
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.Id));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Activity, ActivityDetailsDto>()
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActivityTypeName, opt => opt.MapFrom(src =>
                    src.ActivityType != null ? src.ActivityType.Type : string.Empty))
                .ForMember(dest => dest.ActivityStatusName, opt => opt.MapFrom(src =>
                    src.ActivityStatus != null ? src.ActivityStatus.Status : string.Empty))
                .ForMember(dest => dest.AssignedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTeamName, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedByName, opt => opt.Ignore());

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Activity, ActivityListDto>()
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActivityTypeName, opt => opt.MapFrom(src =>
                    src.ActivityType != null ? src.ActivityType.Type : string.Empty))
                .ForMember(dest => dest.ActivityStatusName, opt => opt.MapFrom(src =>
                    src.ActivityStatus != null ? src.ActivityStatus.Status : string.Empty));

            // Command to Entity mappings
            CreateMap<CreateActivityCommand, VibeCRM.Domain.Entities.BusinessEntities.Activity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.ActivityType, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());

            CreateMap<UpdateActivityCommand, VibeCRM.Domain.Entities.BusinessEntities.Activity>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityType, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());
        }
    }
}