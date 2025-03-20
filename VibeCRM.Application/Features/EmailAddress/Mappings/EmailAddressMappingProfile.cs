using AutoMapper;
using VibeCRM.Application.Features.EmailAddress.Commands.CreateEmailAddress;
using VibeCRM.Application.Features.EmailAddress.Commands.UpdateEmailAddress;
using VibeCRM.Application.Features.EmailAddress.DTOs;

namespace VibeCRM.Application.Features.EmailAddress.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between EmailAddress entities and DTOs.
    /// Defines the mapping configuration for all email address-related objects.
    /// </summary>
    public class EmailAddressMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressMappingProfile"/> class
        /// and configures the mapping relationships.
        /// </summary>
        public EmailAddressMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.EmailAddress, EmailAddressDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmailAddressId));

            CreateMap<Domain.Entities.BusinessEntities.EmailAddress, EmailAddressDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmailAddressId))
                .ForMember(dest => dest.EmailAddressTypeName, opt => opt.MapFrom(src => src.EmailAddressType != null ? src.EmailAddressType.Type : null));

            CreateMap<Domain.Entities.BusinessEntities.EmailAddress, EmailAddressListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmailAddressId))
                .ForMember(dest => dest.EmailAddressTypeName, opt => opt.MapFrom(src => src.EmailAddressType != null ? src.EmailAddressType.Type : null));

            // Command to Entity mappings
            CreateMap<CreateEmailAddressCommand, Domain.Entities.BusinessEntities.EmailAddress>()
                .ForMember(dest => dest.EmailAddressId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddressType, opt => opt.Ignore());

            CreateMap<UpdateEmailAddressCommand, Domain.Entities.BusinessEntities.EmailAddress>()
                .ForMember(dest => dest.EmailAddressId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddressType, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}