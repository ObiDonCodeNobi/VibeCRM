using AutoMapper;
using VibeCRM.Application.Features.Address.Commands.CreateAddress;
using VibeCRM.Application.Features.Address.Commands.UpdateAddress;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Mappings
{
    /// <summary>
    /// Mapping profile for Address entity and related DTOs.
    /// Defines how entities are converted to DTOs and vice versa.
    /// </summary>
    public class AddressMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressMappingProfile"/> class
        /// and configures the entity-to-DTO mappings.
        /// </summary>
        public AddressMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Address, AddressDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddressId));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Address, AddressDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.AddressTypeName, opt => opt.MapFrom(src =>
                    src.AddressType != null ? src.AddressType.Type : string.Empty))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.FullAddress));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Address, AddressListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.AddressTypeName, opt => opt.MapFrom(src =>
                    src.AddressType != null ? src.AddressType.Type : string.Empty))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.FullAddress));

            // Command to Entity mappings
            CreateMap<CreateAddressCommand, VibeCRM.Domain.Entities.BusinessEntities.Address>()
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.AddressType, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());

            CreateMap<UpdateAddressCommand, VibeCRM.Domain.Entities.BusinessEntities.Address>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.AddressType, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());
        }
    }
}