using MediatR;
using VibeCRM.Shared.DTOs.PaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodById
{
    /// <summary>
    /// Query to retrieve a payment method by its ID
    /// </summary>
    public class GetPaymentMethodByIdQuery : IRequest<PaymentMethodDetailsDto>
    {
        /// <summary>
        /// Gets or sets the ID of the payment method to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}