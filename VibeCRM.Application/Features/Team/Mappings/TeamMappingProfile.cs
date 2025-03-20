using AutoMapper;
using VibeCRM.Application.Features.Team.Commands.CreateTeam;
using VibeCRM.Application.Features.Team.Commands.UpdateTeam;
using VibeCRM.Application.Features.Team.DTOs;

namespace VibeCRM.Application.Features.Team.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Team entities and DTOs
    /// </summary>
    public class TeamMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMappingProfile"/> class
        /// </summary>
        public TeamMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Team, TeamDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TeamId));

            CreateMap<Domain.Entities.BusinessEntities.Team, TeamListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.MemberCount, opt => opt.Ignore());

            CreateMap<Domain.Entities.BusinessEntities.Team, TeamDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.MemberCount, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreateTeamCommand, Domain.Entities.BusinessEntities.Team>()
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<UpdateTeamCommand, Domain.Entities.BusinessEntities.Team>()
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}