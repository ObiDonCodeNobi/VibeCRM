using AutoMapper;
using VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.Commands.UpdatePaymentMethod;
using VibeCRM.Application.Features.PaymentMethod.DTOs;

namespace VibeCRM.Application.Features.PaymentMethod.Mapping
{
    /// <summary>
    /// Mapping profile for payment method entities and DTOs
    /// </summary>
    public class PaymentMethodMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethodMappingProfile"/> class
        /// </summary>
        public PaymentMethodMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.PaymentMethod, PaymentMethodDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.PaymentMethod, PaymentMethodDetailsDto>();

            // Command to Entity mappings
            CreateMap<CreatePaymentMethodCommand, Domain.Entities.TypeStatusEntities.PaymentMethod>();
            CreateMap<UpdatePaymentMethodCommand, Domain.Entities.TypeStatusEntities.PaymentMethod>();
        }
    }
}
