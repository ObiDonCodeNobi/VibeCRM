using AutoMapper;
using VibeCRM.Application.Features.User.Commands.CreateUser;
using VibeCRM.Application.Features.User.Commands.UpdateUser;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between User entities and DTOs
    /// </summary>
    public class UserMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMappingProfile"/> class
        /// </summary>
        public UserMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Domain.Entities.BusinessEntities.User, UserListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Domain.Entities.BusinessEntities.User, UserDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

            // Command to Entity mappings
            CreateMap<CreateUserCommand, Domain.Entities.BusinessEntities.User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<UpdateUserCommand, Domain.Entities.BusinessEntities.User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}