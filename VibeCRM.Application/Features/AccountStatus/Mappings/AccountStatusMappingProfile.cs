using AutoMapper;
using VibeCRM.Application.Features.AccountStatus.Commands.CreateAccountStatus;
using VibeCRM.Application.Features.AccountStatus.Commands.UpdateAccountStatus;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between AccountStatus entities and DTOs.
    /// Defines the mapping configuration for all account status-related objects.
    /// </summary>
    public class AccountStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStatusMappingProfile"/> class
        /// and configures the mapping relationships.
        /// </summary>
        public AccountStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.AccountStatus, AccountStatusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountStatusId));

            CreateMap<Domain.Entities.TypeStatusEntities.AccountStatus, AccountStatusDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountStatusId))
                .ForMember(dest => dest.CompanyCount, opt => opt.Ignore());

            CreateMap<Domain.Entities.TypeStatusEntities.AccountStatus, AccountStatusListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountStatusId))
                .ForMember(dest => dest.CompanyCount, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreateAccountStatusCommand, Domain.Entities.TypeStatusEntities.AccountStatus>()
                .ForMember(dest => dest.AccountStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateAccountStatusCommand, Domain.Entities.TypeStatusEntities.AccountStatus>()
                .ForMember(dest => dest.AccountStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}