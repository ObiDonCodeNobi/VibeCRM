using AutoMapper;
using VibeCRM.Application.Features.PersonType.Commands.CreatePersonType;
using VibeCRM.Application.Features.PersonType.Commands.UpdatePersonType;
using VibeCRM.Application.Features.PersonType.DTOs;

namespace VibeCRM.Application.Features.PersonType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between PersonType entities and DTOs.
    /// Defines mapping configurations for all PersonType-related objects.
    /// </summary>
    public class PersonTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the PersonTypeMappingProfile class.
        /// Configures all mappings between PersonType entities, DTOs, and commands.
        /// </summary>
        public PersonTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.PersonType, PersonTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.PersonType, PersonTypeListDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.PersonType, PersonTypeDetailsDto>();

            // Command to Entity mappings
            CreateMap<CreatePersonTypeCommand, Domain.Entities.TypeStatusEntities.PersonType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.CreatedBy) ? Guid.Empty : Guid.Parse(src.CreatedBy)))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.ModifiedBy) ? Guid.Empty : Guid.Parse(src.ModifiedBy)))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.People, opt => opt.Ignore());

            CreateMap<UpdatePersonTypeCommand, Domain.Entities.TypeStatusEntities.PersonType>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.ModifiedBy) ? Guid.Empty : Guid.Parse(src.ModifiedBy)))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.People, opt => opt.Ignore());
        }
    }
}