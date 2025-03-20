using AutoMapper;
using VibeCRM.Application.Features.ShipMethod.Commands.CreateShipMethod;
using VibeCRM.Application.Features.ShipMethod.Commands.UpdateShipMethod;
using VibeCRM.Application.Features.ShipMethod.DTOs;

namespace VibeCRM.Application.Features.ShipMethod.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between ShipMethod entities and DTOs.
    /// </summary>
    public class ShipMethodMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipMethodMappingProfile"/> class.
        /// Configures mappings between ShipMethod entities and DTOs.
        /// </summary>
        public ShipMethodMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.ShipMethod, ShipMethodDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ShipMethodId));

            CreateMap<Domain.Entities.TypeStatusEntities.ShipMethod, ShipMethodListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ShipMethodId))
                .ForMember(dest => dest.OrderCount, opt => opt.Ignore()); // This will be populated separately

            CreateMap<Domain.Entities.TypeStatusEntities.ShipMethod, ShipMethodDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ShipMethodId))
                .ForMember(dest => dest.OrderCount, opt => opt.Ignore()); // This will be populated separately

            // DTO to Entity mappings
            CreateMap<ShipMethodDto, Domain.Entities.TypeStatusEntities.ShipMethod>()
                .ForMember(dest => dest.ShipMethodId, opt => opt.MapFrom(src => src.Id));

            // Command to Entity mappings
            CreateMap<CreateShipMethodCommand, Domain.Entities.TypeStatusEntities.ShipMethod>()
                .ForMember(dest => dest.ShipMethodId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<UpdateShipMethodCommand, Domain.Entities.TypeStatusEntities.ShipMethod>()
                .ForMember(dest => dest.ShipMethodId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}