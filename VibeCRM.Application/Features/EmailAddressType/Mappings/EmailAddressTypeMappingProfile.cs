using AutoMapper;
using VibeCRM.Application.Features.EmailAddressType.Commands.CreateEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Commands.UpdateEmailAddressType;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between EmailAddressType entities and DTOs.
    /// </summary>
    public class EmailAddressTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressTypeMappingProfile"/> class.
        /// </summary>
        public EmailAddressTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.EmailAddressType, EmailAddressTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.EmailAddressType, EmailAddressTypeDetailsDto>()
                .ForMember(dest => dest.EmailAddressCount, opt => opt.MapFrom(src => src.EmailAddresses != null ? src.EmailAddresses.Count : 0));
            CreateMap<Domain.Entities.TypeStatusEntities.EmailAddressType, EmailAddressTypeListDto>()
                .ForMember(dest => dest.EmailAddressCount, opt => opt.MapFrom(src => src.EmailAddresses != null ? src.EmailAddresses.Count : 0));

            // Command to Entity mappings
            CreateMap<CreateEmailAddressTypeCommand, Domain.Entities.TypeStatusEntities.EmailAddressType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddresses, opt => opt.Ignore());

            CreateMap<UpdateEmailAddressTypeCommand, Domain.Entities.TypeStatusEntities.EmailAddressType>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddresses, opt => opt.Ignore());
        }
    }
}