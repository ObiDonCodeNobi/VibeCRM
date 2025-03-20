using AutoMapper;
using VibeCRM.Application.Features.CallDirection.Commands.CreateCallDirection;
using VibeCRM.Application.Features.CallDirection.Commands.UpdateCallDirection;
using VibeCRM.Application.Features.CallDirection.DTOs;

namespace VibeCRM.Application.Features.CallDirection.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between CallDirection entities and DTOs.
    /// Defines the mapping configurations for all CallDirection-related objects.
    /// </summary>
    public class CallDirectionMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDirectionMappingProfile"/> class.
        /// Sets up all mapping configurations for CallDirection-related objects.
        /// </summary>
        public CallDirectionMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.CallDirection, CallDirectionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrdinalPosition, opt => opt.MapFrom(src => src.OrdinalPosition));

            CreateMap<Domain.Entities.TypeStatusEntities.CallDirection, CallDirectionListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrdinalPosition, opt => opt.MapFrom(src => src.OrdinalPosition))
                .ForMember(dest => dest.ActivityCount, opt => opt.Ignore()); // This will need to be populated separately if needed

            CreateMap<Domain.Entities.TypeStatusEntities.CallDirection, CallDirectionDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrdinalPosition, opt => opt.MapFrom(src => src.OrdinalPosition))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.ActivityCount, opt => opt.Ignore()); // This will need to be populated separately if needed

            // Command to Entity mappings
            CreateMap<CreateCallDirectionCommand, Domain.Entities.TypeStatusEntities.CallDirection>()
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrdinalPosition, opt => opt.MapFrom(src => src.OrdinalPosition))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.CallDirectionId, opt => opt.Ignore());

            CreateMap<UpdateCallDirectionCommand, Domain.Entities.TypeStatusEntities.CallDirection>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrdinalPosition, opt => opt.MapFrom(src => src.OrdinalPosition))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.CallDirectionId, opt => opt.Ignore());

            // DTO to DTO mappings
            CreateMap<CallDirectionDto, CallDirectionListDto>()
                .ForMember(dest => dest.ActivityCount, opt => opt.Ignore());

            CreateMap<CallDirectionDto, CallDirectionDetailsDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityCount, opt => opt.Ignore());
        }
    }
}