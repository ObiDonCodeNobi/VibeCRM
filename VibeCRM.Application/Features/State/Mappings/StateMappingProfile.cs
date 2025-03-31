using AutoMapper;
using VibeCRM.Application.Features.State.Commands.CreateState;
using VibeCRM.Application.Features.State.Commands.UpdateState;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between State entities and DTOs.
    /// </summary>
    public class StateMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMappingProfile"/> class.
        /// Configures mappings between State entities and various DTOs.
        /// </summary>
        public StateMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.State, StateDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StateId));

            CreateMap<Domain.Entities.TypeStatusEntities.State, StateListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.AddressCount, opt => opt.MapFrom(src => src.Addresses != null ? src.Addresses.Count : 0));

            CreateMap<Domain.Entities.TypeStatusEntities.State, StateDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.AddressCount, opt => opt.MapFrom(src => src.Addresses != null ? src.Addresses.Count : 0));

            // Command to Entity mappings
            CreateMap<CreateStateCommand, Domain.Entities.TypeStatusEntities.State>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore());

            CreateMap<UpdateStateCommand, Domain.Entities.TypeStatusEntities.State>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore());
        }
    }
}