using AutoMapper;
using VibeCRM.Application.Features.PersonStatus.Commands.CreatePersonStatus;
using VibeCRM.Application.Features.PersonStatus.Commands.UpdatePersonStatus;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between PersonStatus entities and DTOs.
    /// Configures mappings for all PersonStatus-related objects.
    /// </summary>
    public class PersonStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonStatusMappingProfile"/> class.
        /// Configures all mappings between PersonStatus entities and DTOs.
        /// </summary>
        public PersonStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.PersonStatus, PersonStatusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonStatusId));

            CreateMap<Domain.Entities.TypeStatusEntities.PersonStatus, PersonStatusDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonStatusId))
                .ForMember(dest => dest.PeopleCount, opt => opt.Ignore()); // Set manually in handlers

            CreateMap<Domain.Entities.TypeStatusEntities.PersonStatus, PersonStatusListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonStatusId))
                .ForMember(dest => dest.PeopleCount, opt => opt.Ignore()); // Set manually in handlers

            // Command to Entity mappings
            CreateMap<CreatePersonStatusCommand, Domain.Entities.TypeStatusEntities.PersonStatus>()
                .ForMember(dest => dest.PersonStatusId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.CreatedBy) ? Guid.Parse(src.CreatedBy) : Guid.Empty))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ModifiedBy) ? Guid.Parse(src.ModifiedBy) : Guid.Empty));

            CreateMap<UpdatePersonStatusCommand, Domain.Entities.TypeStatusEntities.PersonStatus>()
                .ForMember(dest => dest.PersonStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ModifiedBy) ? Guid.Parse(src.ModifiedBy) : Guid.Empty));
        }
    }
}