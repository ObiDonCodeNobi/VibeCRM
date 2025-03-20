using AutoMapper;
using VibeCRM.Application.Features.ActivityDefinition.Commands.CreateActivityDefinition;
using VibeCRM.Application.Features.ActivityDefinition.Commands.UpdateActivityDefinition;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;

namespace VibeCRM.Application.Features.ActivityDefinition.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ActivityDefinition entities and related DTOs.
    /// Defines mapping configurations for entity-to-DTO and DTO-to-entity conversions.
    /// </summary>
    public class ActivityDefinitionMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDefinitionMappingProfile"/> class.
        /// Configures mappings between ActivityDefinition entities and DTOs.
        /// </summary>
        public ActivityDefinitionMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.ActivityDefinition, ActivityDefinitionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActivityDefinitionId));

            CreateMap<Domain.Entities.BusinessEntities.ActivityDefinition, ActivityDefinitionDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActivityDefinitionId))
                .ForMember(dest => dest.ActivityTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityStatusName, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTeamName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByName, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedByName, opt => opt.Ignore());

            CreateMap<Domain.Entities.BusinessEntities.ActivityDefinition, ActivityDefinitionListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActivityDefinitionId))
                .ForMember(dest => dest.ActivityTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityStatusName, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTeamName, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreateActivityDefinitionCommand, Domain.Entities.BusinessEntities.ActivityDefinition>()
                .ForMember(dest => dest.ActivityDefinitionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());

            CreateMap<UpdateActivityDefinitionCommand, Domain.Entities.BusinessEntities.ActivityDefinition>()
                .ForMember(dest => dest.ActivityDefinitionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
        }
    }
}