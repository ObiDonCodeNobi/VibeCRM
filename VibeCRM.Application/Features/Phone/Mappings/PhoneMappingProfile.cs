using AutoMapper;
using VibeCRM.Application.Features.Phone.Commands.CreatePhone;
using VibeCRM.Application.Features.Phone.Commands.UpdatePhone;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Phone entity and related DTOs
    /// </summary>
    public class PhoneMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneMappingProfile"/> class
        /// </summary>
        public PhoneMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Phone, PhoneDto>()
                .ForMember(dest => dest.FormattedPhoneNumber, opt => opt.MapFrom(src =>
                    $"({src.AreaCode}) {src.Prefix}-{src.LineNumber}" + (src.Extension.HasValue ? $" ext.{src.Extension.Value}" : "")));

            CreateMap<Domain.Entities.BusinessEntities.Phone, PhoneListDto>()
                .ForMember(dest => dest.PhoneTypeName, opt => opt.MapFrom(src => src.PhoneType != null ? src.PhoneType.Type : string.Empty))
                .ForMember(dest => dest.FormattedPhoneNumber, opt => opt.MapFrom(src =>
                    $"({src.AreaCode}) {src.Prefix}-{src.LineNumber}" + (src.Extension.HasValue ? $" ext.{src.Extension.Value}" : "")));

            CreateMap<Domain.Entities.BusinessEntities.Phone, PhoneDetailsDto>()
                .ForMember(dest => dest.PhoneTypeName, opt => opt.MapFrom(src => src.PhoneType != null ? src.PhoneType.Type : string.Empty))
                .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Companies))
                .ForMember(dest => dest.Persons, opt => opt.MapFrom(src => src.Persons));

            CreateMap<Company_Phone, CompanyPhoneDto>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : string.Empty));

            CreateMap<Person_Phone, PersonPhoneDto>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Person != null ?
                    $"{src.Person.Firstname} {src.Person.Lastname}" : string.Empty));

            // DTO to Entity mappings
            CreateMap<PhoneDto, Domain.Entities.BusinessEntities.Phone>();
            CreateMap<PhoneListDto, Domain.Entities.BusinessEntities.Phone>();
            CreateMap<PhoneDetailsDto, Domain.Entities.BusinessEntities.Phone>();

            // Command to Entity mappings
            CreateMap<CreatePhoneCommand, Domain.Entities.BusinessEntities.Phone>();
            CreateMap<UpdatePhoneCommand, Domain.Entities.BusinessEntities.Phone>();
        }
    }
}