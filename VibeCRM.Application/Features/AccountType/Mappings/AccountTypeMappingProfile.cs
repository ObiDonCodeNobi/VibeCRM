using VibeCRM.Application.Features.AccountType.Commands.CreateAccountType;
using VibeCRM.Application.Features.AccountType.Commands.UpdateAccountType;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Features.AccountType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between AccountType entity and its DTOs.
    /// </summary>
    public class AccountTypeMappingProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTypeMappingProfile"/> class.
        /// Configures the mapping between AccountType entities and their corresponding DTOs.
        /// </summary>
        public AccountTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.AccountType, AccountTypeDto>();
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.AccountType, AccountTypeListDto>();
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.AccountType, AccountTypeDetailsDto>();

            // DTO to Entity mappings
            CreateMap<AccountTypeDto, VibeCRM.Domain.Entities.TypeStatusEntities.AccountType>();

            // Command to Entity mappings
            CreateMap<CreateAccountTypeCommand, VibeCRM.Domain.Entities.TypeStatusEntities.AccountType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => System.Guid.NewGuid()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateAccountTypeCommand, VibeCRM.Domain.Entities.TypeStatusEntities.AccountType>();
        }
    }
}