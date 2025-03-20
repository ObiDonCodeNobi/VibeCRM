using AutoMapper;
using VibeCRM.Application.Features.PhoneType.Commands.CreatePhoneType;
using VibeCRM.Application.Features.PhoneType.Commands.UpdatePhoneType;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between PhoneType entities and DTOs.
    /// </summary>
    public class PhoneTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneTypeMappingProfile"/> class.
        /// Configures mappings between PhoneType entities and DTOs.
        /// </summary>
        public PhoneTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.PhoneType, PhoneTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.PhoneType, PhoneTypeListDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.PhoneType, PhoneTypeDetailsDto>();

            // Command to Entity mappings
            CreateMap<CreatePhoneTypeCommand, Domain.Entities.TypeStatusEntities.PhoneType>();
            CreateMap<UpdatePhoneTypeCommand, Domain.Entities.TypeStatusEntities.PhoneType>();

            // DTO to Entity mappings
            CreateMap<PhoneTypeDto, Domain.Entities.TypeStatusEntities.PhoneType>();
        }
    }
}
