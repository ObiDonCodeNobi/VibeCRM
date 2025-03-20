using MediatR;
using VibeCRM.Application.Features.PaymentMethod.DTOs;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetDefaultPaymentMethod
{
    /// <summary>
    /// Query to retrieve the default payment method
    /// </summary>
    public class GetDefaultPaymentMethodQuery : IRequest<PaymentMethodDto>
    {
        // No parameters needed for retrieving the default payment method
    }
}