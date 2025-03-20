using AutoMapper;
using VibeCRM.Application.Features.Role.Commands.CreateRole;
using VibeCRM.Application.Features.Role.Commands.UpdateRole;
using VibeCRM.Application.Features.Role.DTOs;

namespace VibeCRM.Application.Features.Role.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Role entities and DTOs
    /// </summary>
    public class RoleMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleMappingProfile"/> class
        /// </summary>
        public RoleMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Domain.Entities.BusinessEntities.Role, RoleDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Domain.Entities.BusinessEntities.Role, RoleListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            // Command to Entity mappings
            CreateMap<CreateRoleCommand, Domain.Entities.BusinessEntities.Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

            CreateMap<UpdateRoleCommand, Domain.Entities.BusinessEntities.Role>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());
        }
    }
}