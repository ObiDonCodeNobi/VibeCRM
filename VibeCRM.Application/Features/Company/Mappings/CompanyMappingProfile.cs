using AutoMapper;
using VibeCRM.Application.Features.Company.Commands.CreateCompany;
using VibeCRM.Application.Features.Company.Commands.UpdateCompany;
using VibeCRM.Application.Features.Company.DTOs;

namespace VibeCRM.Application.Features.Company.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Company entities and DTOs.
    /// Defines the mapping configuration for all company-related objects.
    /// </summary>
    public class CompanyMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyMappingProfile"/> class
        /// and configures the mapping relationships.
        /// </summary>
        public CompanyMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Company, CompanyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompanyId));

            CreateMap<Domain.Entities.BusinessEntities.Company, CompanyDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.ParentCompanyName, opt => opt.MapFrom(src => src.ParentCompany != null ? src.ParentCompany.Name : null))
                .ForMember(dest => dest.AccountTypeName, opt => opt.MapFrom(src => src.AccountType != null ? src.AccountType.Type : null))
                .ForMember(dest => dest.AccountStatusName, opt => opt.MapFrom(src => src.AccountStatus != null ? src.AccountStatus.Status : null))
                .ForMember(dest => dest.PrimaryContactName, opt => opt.MapFrom(src => src.PrimaryContact != null ? $"{src.PrimaryContact.Firstname} {src.PrimaryContact.Lastname}" : null))
                .ForMember(dest => dest.PrimaryPhoneNumber, opt => opt.MapFrom(src => src.PrimaryPhone != null ?
                    $"({src.PrimaryPhone.AreaCode}) {src.PrimaryPhone.Prefix}-{src.PrimaryPhone.LineNumber}" : null))
                .ForMember(dest => dest.PrimaryAddressFormatted, opt => opt.MapFrom(src => src.PrimaryAddress != null ?
                    $"{src.PrimaryAddress.Line1}, {src.PrimaryAddress.City}, {(src.PrimaryAddress.State != null ? src.PrimaryAddress.State.Abbreviation : string.Empty)} {src.PrimaryAddress.Zip}" : null));

            CreateMap<Domain.Entities.BusinessEntities.Company, CompanyListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.AccountTypeName, opt => opt.MapFrom(src => src.AccountType != null ? src.AccountType.Type : null))
                .ForMember(dest => dest.AccountStatusName, opt => opt.MapFrom(src => src.AccountStatus != null ? src.AccountStatus.Status : null))
                .ForMember(dest => dest.PrimaryContactName, opt => opt.MapFrom(src => src.PrimaryContact != null ? $"{src.PrimaryContact.Firstname} {src.PrimaryContact.Lastname}" : null));

            // Command to Entity mappings
            CreateMap<CreateCompanyCommand, Domain.Entities.BusinessEntities.Company>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.Phones, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddresses, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ForMember(dest => dest.Quotes, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrders, opt => opt.Ignore())
                .ForMember(dest => dest.Calls, opt => opt.Ignore())
                .ForMember(dest => dest.ChildCompanies, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCompany, opt => opt.Ignore())
                .ForMember(dest => dest.AccountType, opt => opt.Ignore())
                .ForMember(dest => dest.AccountStatus, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryContact, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryPhone, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryAddress, opt => opt.Ignore());

            CreateMap<UpdateCompanyCommand, Domain.Entities.BusinessEntities.Company>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.Phones, opt => opt.Ignore())
                .ForMember(dest => dest.EmailAddresses, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ForMember(dest => dest.Quotes, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrders, opt => opt.Ignore())
                .ForMember(dest => dest.Calls, opt => opt.Ignore())
                .ForMember(dest => dest.ChildCompanies, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCompany, opt => opt.Ignore())
                .ForMember(dest => dest.AccountType, opt => opt.Ignore())
                .ForMember(dest => dest.AccountStatus, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryContact, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryPhone, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryAddress, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}