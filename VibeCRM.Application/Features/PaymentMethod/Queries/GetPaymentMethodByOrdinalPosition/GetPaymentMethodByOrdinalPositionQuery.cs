using MediatR;
using VibeCRM.Shared.DTOs.PaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve payment methods ordered by their ordinal position
    /// </summary>
    public class GetPaymentMethodByOrdinalPositionQuery : IRequest<IEnumerable<PaymentMethodDto>>
    {
        // No parameters needed for retrieving payment methods by ordinal position
    }
}