using AutoMapper;
using VibeCRM.Application.Features.CallType.Commands.CreateCallType;
using VibeCRM.Application.Features.CallType.Commands.UpdateCallType;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between CallType entities and DTOs.
    /// Defines mapping configurations for all CallType-related types.
    /// </summary>
    public class CallTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallTypeMappingProfile"/> class.
        /// Sets up mapping configurations for CallType entities and DTOs.
        /// </summary>
        public CallTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.CallType, CallTypeDto>();

            CreateMap<Domain.Entities.TypeStatusEntities.CallType, CallTypeListDto>()
                .ForMember(dest => dest.CallCount, opt => opt.MapFrom(src => src.Calls.Count))
                .ForMember(dest => dest.IsInbound, opt => opt.MapFrom(src =>
                    src.Type.Contains("Inbound") || src.Type.Contains("Incoming") || src.Type.Contains("Received")))
                .ForMember(dest => dest.IsOutbound, opt => opt.MapFrom(src =>
                    src.Type.Contains("Outbound") || src.Type.Contains("Outgoing") || src.Type.Contains("Made")));

            CreateMap<Domain.Entities.TypeStatusEntities.CallType, CallTypeDetailsDto>()
                .ForMember(dest => dest.CallCount, opt => opt.MapFrom(src => src.Calls.Count))
                .ForMember(dest => dest.IsInbound, opt => opt.MapFrom(src =>
                    src.Type.Contains("Inbound") || src.Type.Contains("Incoming") || src.Type.Contains("Received")))
                .ForMember(dest => dest.IsOutbound, opt => opt.MapFrom(src =>
                    src.Type.Contains("Outbound") || src.Type.Contains("Outgoing") || src.Type.Contains("Made")));

            // Command to Entity mappings
            CreateMap<CreateCallTypeCommand, Domain.Entities.TypeStatusEntities.CallType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CallTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Calls, opt => opt.Ignore());

            CreateMap<UpdateCallTypeCommand, Domain.Entities.TypeStatusEntities.CallType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CallTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Calls, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}