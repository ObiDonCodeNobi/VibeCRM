using AutoMapper;
using VibeCRM.Application.Features.PaymentStatus.Commands.CreatePaymentStatus;
using VibeCRM.Application.Features.PaymentStatus.Commands.UpdatePaymentStatus;
using VibeCRM.Shared.DTOs.PaymentStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between PaymentStatus entities and DTOs.
    /// Defines the mapping configuration for all payment status-related objects.
    /// </summary>
    public class PaymentStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatusMappingProfile"/> class
        /// and configures the mapping relationships.
        /// </summary>
        public PaymentStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.PaymentStatus, PaymentStatusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentStatusId));

            CreateMap<Domain.Entities.TypeStatusEntities.PaymentStatus, PaymentStatusDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentStatusId))
                .ForMember(dest => dest.PaymentCount, opt => opt.Ignore());

            CreateMap<Domain.Entities.TypeStatusEntities.PaymentStatus, PaymentStatusListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentStatusId))
                .ForMember(dest => dest.PaymentCount, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreatePaymentStatusCommand, Domain.Entities.TypeStatusEntities.PaymentStatus>()
                .ForMember(dest => dest.PaymentStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdatePaymentStatusCommand, Domain.Entities.TypeStatusEntities.PaymentStatus>()
                .ForMember(dest => dest.PaymentStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}