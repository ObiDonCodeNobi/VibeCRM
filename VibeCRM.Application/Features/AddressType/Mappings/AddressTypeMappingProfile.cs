using AutoMapper;
using VibeCRM.Application.Features.AddressType.Commands.CreateAddressType;
using VibeCRM.Application.Features.AddressType.Commands.UpdateAddressType;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between AddressType entities and DTOs.
    /// </summary>
    public class AddressTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressTypeMappingProfile"/> class.
        /// Configures mappings between AddressType entities and DTOs.
        /// </summary>
        public AddressTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.AddressType, AddressTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.AddressType, AddressTypeListDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.AddressType, AddressTypeDetailsDto>();

            // Command to Entity mappings
            CreateMap<CreateAddressTypeCommand, Domain.Entities.TypeStatusEntities.AddressType>();
            CreateMap<UpdateAddressTypeCommand, Domain.Entities.TypeStatusEntities.AddressType>();

            // DTO to Entity mappings
            CreateMap<AddressTypeDto, Domain.Entities.TypeStatusEntities.AddressType>();
        }
    }
}