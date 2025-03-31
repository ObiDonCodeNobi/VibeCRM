using AutoMapper;
using VibeCRM.Application.Features.ContactType.Commands.CreateContactType;
using VibeCRM.Application.Features.ContactType.Commands.UpdateContactType;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ContactType entities and DTOs.
    /// </summary>
    public class ContactTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactTypeMappingProfile"/> class.
        /// Configures mappings between ContactType entities and DTOs.
        /// </summary>
        public ContactTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.ContactType, ContactTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.ContactType, ContactTypeListDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.ContactType, ContactTypeDetailsDto>();

            // Command to Entity mappings
            CreateMap<CreateContactTypeCommand, Domain.Entities.TypeStatusEntities.ContactType>();
            CreateMap<UpdateContactTypeCommand, Domain.Entities.TypeStatusEntities.ContactType>();

            // DTO to Entity mappings
            CreateMap<ContactTypeDto, Domain.Entities.TypeStatusEntities.ContactType>();
        }
    }
}