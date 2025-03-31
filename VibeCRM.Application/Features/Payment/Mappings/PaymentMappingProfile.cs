using AutoMapper;
using VibeCRM.Application.Features.Payment.Commands.CreatePayment;
using VibeCRM.Application.Features.Payment.Commands.UpdatePayment;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Payment entities and DTOs.
    /// This profile defines mapping configurations for converting between domain entities and data transfer objects.
    /// </summary>
    public class PaymentMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMappingProfile"/> class.
        /// Configures the mapping between Payment entities and various DTOs.
        /// </summary>
        public PaymentMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Payment, PaymentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId));

            CreateMap<Domain.Entities.BusinessEntities.Payment, PaymentDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.PaymentTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentMethodTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStatusName, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyName, opt => opt.Ignore())
                .ForMember(dest => dest.PersonName, opt => opt.Ignore());

            CreateMap<Domain.Entities.BusinessEntities.Payment, PaymentListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.PaymentTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStatusName, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceNumber, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreatePaymentCommand, Domain.Entities.BusinessEntities.Payment>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.PaymentLineItems, opt => opt.Ignore());

            CreateMap<UpdatePaymentCommand, Domain.Entities.BusinessEntities.Payment>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentLineItems, opt => opt.Ignore());
        }
    }
}