using AutoMapper;
using VibeCRM.Application.Features.Call.Commands.CreateCall;
using VibeCRM.Application.Features.Call.Commands.UpdateCall;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Call entities and DTOs.
    /// Defines the mapping configuration for all Call-related objects.
    /// </summary>
    public class CallMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallMappingProfile"/> class.
        /// Configures the mappings between Call entities and DTOs.
        /// </summary>
        public CallMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Call, CallDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CallId));

            CreateMap<Domain.Entities.BusinessEntities.Call, CallDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CallId))
                .ForMember(dest => dest.IsInbound, opt => opt.MapFrom(src => src.IsInbound));

            CreateMap<Domain.Entities.BusinessEntities.Call, CallListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CallId))
                .ForMember(dest => dest.IsInbound, opt => opt.MapFrom(src => src.IsInbound));

            // Command to Entity mappings
            CreateMap<CreateCallCommand, Domain.Entities.BusinessEntities.Call>()
                .ForMember(dest => dest.CallId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());

            CreateMap<UpdateCallCommand, Domain.Entities.BusinessEntities.Call>()
                .ForMember(dest => dest.CallId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());
        }
    }
}