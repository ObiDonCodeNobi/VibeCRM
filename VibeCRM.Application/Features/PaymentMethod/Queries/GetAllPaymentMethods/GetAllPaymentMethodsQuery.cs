using MediatR;
using VibeCRM.Application.Features.PaymentMethod.DTOs;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetAllPaymentMethods
{
    /// <summary>
    /// Query to retrieve all payment methods
    /// </summary>
    public class GetAllPaymentMethodsQuery : IRequest<IEnumerable<PaymentMethodDto>>
    {
        // No parameters needed for retrieving all payment methods
    }
}